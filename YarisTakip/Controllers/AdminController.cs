using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.Services;
using YarisTakip.ViewModel;

namespace YarisTakip.Controllers
{
    public class AdminController : Controller
    {
        private readonly IYarisRepository _yarisRespository;
        private readonly IKullaniciRepository _kullaniciRepository;

        public AdminController(IYarisRepository yarisRespository, IKullaniciRepository kullaniciRepository)
        {
            _yarisRespository = yarisRespository;
            _kullaniciRepository = kullaniciRepository;
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

    }
}
