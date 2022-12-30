using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.ViewModel;

namespace YarisTakip.Controllers
{
    public class YarisController : Controller
    {
        private readonly IYarisRepository _yarisRespository;
        private readonly IResimService _resimService;

        public YarisController(IYarisRepository yarisRespository, IResimService resimService)
        {
            _yarisRespository = yarisRespository;
            _resimService = resimService;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateYarisViewModel yarisVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _resimService.AddPhotoAsync(yarisVM.Resim);

                var yaris = new Yaris
                {
                    Baslik = yarisVM.Baslik,
                    Aciklama = yarisVM.Aciklama,
                    Resim = result.Url.ToString(),
                    Adres = new Adres
                    {
                        Sokak = yarisVM.Adres.Sokak,
                        Sehir = yarisVM.Adres.Sehir,
                        Ulke = yarisVM.Adres.Ulke,
                    }
                };
                _yarisRespository.Add(yaris);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(yarisVM);
        }
    }
}
