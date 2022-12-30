using Microsoft.AspNetCore.Mvc;
using YarisTakip.Interfaces;
using YarisTakip.Models;

namespace YarisTakip.Controllers
{
    public class YarisController : Controller
    {
        private readonly IYarisRepository _yarisRespository;

        public YarisController(IYarisRepository yarisRespository)
        {
            _yarisRespository = yarisRespository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Yaris> yarislar = await _yarisRespository.GetAll();
            return View(yarislar);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Yaris yaris = await _yarisRespository.GetByIdAsync(id);
            return View(yaris);
        }
    }
}
