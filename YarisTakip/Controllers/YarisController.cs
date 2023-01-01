using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics;
using System.Net;
using YarisTakip.Data;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.ViewModel;

namespace YarisTakip.Controllers
{
    public class YarisController : Controller
    {
        private readonly IYarisRepository _yarisRespository;
        private readonly IResimService _resimService;
        private readonly AppDbContext _context;

        public YarisController(IYarisRepository yarisRespository, IResimService resimService, AppDbContext context)
        {
            _yarisRespository = yarisRespository;
            _resimService = resimService;
            _context = context;
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
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var yaris = await _yarisRespository.GetByIdAsync(id);
            if (yaris == null) return View("Error");
            var clubVM = new EditYarisViewModel
            {
                Baslik = yaris.Baslik,
                Aciklama = yaris.Aciklama,
                AdresId = (int)yaris.AdresId,
                Adres = yaris.Adres,
                URL = yaris.Resim,
                YarisKategori = yaris.YarisKategori
            };
            return View(clubVM);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditYarisViewModel yarisVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View("Edit", yarisVM);
            }

            var kullaniciYaris = await _yarisRespository.GetByIdAsyncNoTracking(id);

            if (kullaniciYaris != null)
            {
                try
                {
                    await _resimService.DeletePhotoAsync(kullaniciYaris.Resim);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(yarisVM);
                }
                var photoResult = await _resimService.AddPhotoAsync(yarisVM.Resim);


                var yaris = new Yaris
                {
                    Id = id,
                    Baslik = yarisVM.Baslik,
                    Aciklama = yarisVM.Aciklama,
                    Resim = photoResult.Url.ToString(),
                    AdresId = yarisVM.AdresId,
                    Adres = yarisVM.Adres,
                    YarisKategori = yarisVM.YarisKategori
                };

                _yarisRespository.Update(yaris);

                return RedirectToAction("Index");
            }
            else
            {
                return View(yarisVM);
            }
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int Id)
        {
            var yarisDetails = await _yarisRespository.GetByIdAsync(Id);
            if (yarisDetails == null) return RedirectToAction("Yarislar", "Admin");
            var appContext =  _context.KullaniciYarisi.FirstOrDefault(k => k.YarisId == yarisDetails.Id);
            if (appContext == null)
            {
                _context.Remove(yarisDetails);
                _context.SaveChanges();
                return RedirectToAction("Yarislar", "Admin");
            }
                return RedirectToAction("Yarislar", "Admin");
        }
    }
}
