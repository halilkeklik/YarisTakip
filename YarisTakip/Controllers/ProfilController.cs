using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YarisTakip.Data;
using YarisTakip.Models;
using YarisTakip.ViewModel;

namespace YarisTakip.Controllers
{
    public class ProfilController : Controller
    {
        private readonly UserManager<Kullanici> _kullaniciManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly AppDbContext _context;
        public ProfilController(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager, AppDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _kullaniciManager = userManager;
        }

        public IActionResult Giris()
        {
            var response = new GirisViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Giris(GirisViewModel girisViewModel)
        {
            if (!ModelState.IsValid) return View(girisViewModel);

            var kullanici = await _kullaniciManager.FindByEmailAsync(girisViewModel.EmailAdres);

            if (kullanici != null)
            {
                var sifreOnayı = await _kullaniciManager.CheckPasswordAsync(kullanici, girisViewModel.Sifre);
                if (sifreOnayı)
                {
                    var result = await _signInManager.PasswordSignInAsync(kullanici, girisViewModel.Sifre, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Yaris");
                    }
                }
                TempData["Error"] = "Lütfen Tekrar Deneyiniz";
                return View(girisViewModel);
            }
            TempData["Error"] = "Böyle bir kullanıcı yok";
            return View(girisViewModel);
        }

        public IActionResult Kayit()
        {
            var response = new KayitViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Kayit(KayitViewModel kayitViewModel)
        {
            if (!ModelState.IsValid) return View(kayitViewModel);

            var kullanici = await _kullaniciManager.FindByEmailAsync(kayitViewModel.EmailAdres);
            if (kullanici != null)
            {
                TempData["Error"] = "Bu email kullanılmaktadır";
                return View(kayitViewModel);
            }

            var yeniKullanici = new Kullanici()
            {
                Email = kayitViewModel.EmailAdres,
                UserName = kayitViewModel.EmailAdres
            };
            var yenikullaniciResponse = await _kullaniciManager.CreateAsync(yeniKullanici, kayitViewModel.Sifre);

            if (yenikullaniciResponse.Succeeded)
                await _kullaniciManager.AddToRoleAsync(yeniKullanici, KullaniciRolleri.Kullanici);

            return RedirectToAction("Index", "Yaris");
        }

        [HttpPost]
        public async Task<IActionResult> Cikis()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Yaris");
        }
    }
}
