using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.Services;

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
            IEnumerable<Kullanici> kullanicilar = await _kullaniciRepository.GetAllUsers();
            return View(kullanicilar);
        }

    }
}
