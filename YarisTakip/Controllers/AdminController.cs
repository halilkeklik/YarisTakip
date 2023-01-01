using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarisTakip.Interfaces;
using CloudinaryDotNet.Actions;
using YarisTakip.Models;
using YarisTakip.Services;
using YarisTakip.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace YarisTakip.Controllers
{
    [Authorize(Roles = "admin")]
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

        public  ActionResult KullaniciSil(string id)
        {
            var baseAddress = new Uri("https://localhost");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client=new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:7119/api/");
                cookieContainer.Add(baseAddress, new Cookie(".AspNetCore.Identity.Application", Request.Cookies[".AspNetCore.Identity.Application"]));
                var deleteTask = client.DeleteAsync("Kullanici/" + id);
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Kullanicilar");
            }
            return RedirectToAction("Kullanicilar");
        }

    }
}
