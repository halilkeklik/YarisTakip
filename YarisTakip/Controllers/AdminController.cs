using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarisTakip.Interfaces;
using CloudinaryDotNet.Actions;
using YarisTakip.Models;
using YarisTakip.Services;
using YarisTakip.ViewModel;
using System.Security.Claims;

namespace YarisTakip.Controllers
{
    public class AdminController : Controller
    {
        private readonly IYarisRepository _yarisRespository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IResimService _resimService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(IYarisRepository yarisRespository, IKullaniciRepository kullaniciRepository, IHttpContextAccessor httpContextAccessor, IResimService resimService)
        {
            _yarisRespository = yarisRespository;
            _kullaniciRepository = kullaniciRepository;
            _httpContextAccessor = httpContextAccessor;
            _resimService= resimService;
        }
        public async Task<IActionResult> Yarislar()
        {
            IEnumerable<Yaris> yarislar = await _yarisRespository.GetAll();
            return View(yarislar);
        }
        public async Task<IActionResult> Kullanicilar()
        {
            var kullanici = await _kullaniciRepository.GetAllUsers();
            List<KullaniciViewModel> result = new List<KullaniciViewModel>();
            foreach (var user in kullanici)
            {
                var kullaniciViewModel = new KullaniciViewModel()
                {
                    Id = user.Id,
                    KullaniciAdi = user.UserName,
                    KosuHizi = user.KosuHizi,
                    Mesafe = user.Mesafe,
                };
                result.Add(kullaniciViewModel);
            }
            return View(result);
        }

        private void MapUserEdit(Kullanici kullanici, EditKullaniciViewModel editVM, ImageUploadResult photoResult)
        {
            kullanici.Id = editVM.Id;
            kullanici.KosuHizi = editVM.KosuHizi;
            kullanici.Mesafe = editVM.Mesafe;
            kullanici.ProfilResimUrl = photoResult.Url.ToString();
        }

        public async Task<IActionResult> EditKullanici()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var kullanici = await _kullaniciRepository.GetUserById(curUserId);
            if (kullanici == null) return View("Error");
            var editUserViewModel = new EditKullaniciViewModel()
            {
                Id = curUserId,
                KosuHizi = kullanici.KosuHizi,
                Mesafe = kullanici.Mesafe,
                ProfilResimURL = kullanici.ProfilResimUrl
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditKullanici(EditKullaniciViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditKullanici", editVM);
            }

            Kullanici kullanici = await _kullaniciRepository.GetUserById(editVM.Id);

            if (kullanici.ProfilResimUrl == "" || kullanici.ProfilResimUrl == null)
            {

                var photoResult = await _resimService.AddPhotoAsync(editVM.Resim);

                MapUserEdit(kullanici, editVM, photoResult);

                _kullaniciRepository.Update(kullanici);

                return RedirectToAction("Kullanicilar");
            }
            else
            {
                try
                {
                    await _resimService.DeletePhotoAsync(kullanici.ProfilResimUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _resimService.AddPhotoAsync(editVM.Resim);

                MapUserEdit(kullanici, editVM, photoResult);

                _kullaniciRepository.Update(kullanici);

                return RedirectToAction("Kullanicilar");
            }
        }

    }
}
