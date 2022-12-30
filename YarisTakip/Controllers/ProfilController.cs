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
                //User is found, check password
                var passwordCheck = await _kullaniciManager.CheckPasswordAsync(kullanici, girisViewModel.Sifre);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(kullanici, girisViewModel.Sifre, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Yaris");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Lütfen Tekrar Deneyiniz";
                return View(girisViewModel);
            }
            //User not found
            TempData["Error"] = "Lütfen Tekrar Deneyiniz";
            return View(girisViewModel);
        }
    }
}
