using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.Services;
using YarisTakip.ViewModel;

namespace YarisTakip.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IResimService _resimService;

        public KullaniciController(IKullaniciRepository kullaniciRepository, IHttpContextAccessor httpContextAccessor, IResimService resimService)
        {
            _kullaniciRepository = kullaniciRepository;
            _httpContextAccessor = httpContextAccessor;
            _resimService = resimService;
        }

        public async Task<IActionResult> Profil(string id)
        {
            var kullaniciId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (kullaniciId != id)
            {
                return RedirectToAction("Giris", "Hesap");
            }
            var kullanici = await _kullaniciRepository.GetUserById(id);
            var kullaniciDetayViewModel = new KullaniciDetayViewModel()
            {
                Id = kullanici.Id,
                KullaniciAdi = kullanici.UserName,
                KosuHizi = kullanici.KosuHizi,
                Mesafe = kullanici.Mesafe,
                ProfilResimURL = kullanici.ProfilResimUrl
            };
            return View(kullaniciDetayViewModel);
        }

        private void MapUserEdit(Kullanici kullanici, EditKullaniciViewModel editVM, ImageUploadResult photoResult)
        {
            kullanici.Id = editVM.Id;
            kullanici.KosuHizi = editVM.KosuHizi;
            kullanici.Mesafe = editVM.Mesafe;
            kullanici.ProfilResimUrl = photoResult.Url.ToString();
        }

        public async Task<IActionResult> EditKullanici(string id)
        {
            var kullaniciId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (kullaniciId != id)
            {
                return RedirectToAction("Giris", "Hesap");
            }
            var kullanici = await _kullaniciRepository.GetUserById(kullaniciId);
            if (kullanici == null) return View("Error");
            var editUserViewModel = new EditKullaniciViewModel()
            {
                Id = kullaniciId,
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

                return RedirectToAction("Index", "Yaris");
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

                return RedirectToAction("Index", "Yaris");
            }
        }
    }
}
