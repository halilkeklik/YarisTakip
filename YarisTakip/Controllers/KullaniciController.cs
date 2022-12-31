using Microsoft.AspNetCore.Mvc;
using YarisTakip.Interfaces;
using YarisTakip.ViewModel;

namespace YarisTakip.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciController(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        public async Task<IActionResult> Profil(string id)
        {
            var kullanici = await _kullaniciRepository.GetUserById(id);
            var kullaniciDetayViewModel = new KullaniciDetayViewModel()
            {
                Id = kullanici.Id,
                KullaniciAdi = kullanici.UserName,
                KosuHizi = kullanici.KosuHizi,
                Mesafe = kullanici.Mesafe,
            };
            return View(kullaniciDetayViewModel);
        }
    }
}
