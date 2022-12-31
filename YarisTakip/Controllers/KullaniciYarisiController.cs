using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Versioning;
using YarisTakip.Data;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.Repository;

namespace YarisTakip.Controllers
{
    public class KullaniciYarisiController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IYarisRepository _yarisRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KullaniciYarisiController(AppDbContext context, IKullaniciRepository kullaniciRepository, IYarisRepository yarisRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _kullaniciRepository = kullaniciRepository;
            _yarisRepository = yarisRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index(string id)
        {
            var kullanici = await _kullaniciRepository.GetUserById(id);
            var appDbContext = _context.KullaniciYarisi.Where(k => k.KullaniciId == kullanici.Id).Include(k => k.Kullanici).Include(k => k.Yaris);
            return View(await appDbContext.ToListAsync());
        }

        public IActionResult YarisKatil(int Id)
        {
            var kullaniciId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var kullaniciYaris = new KullaniciYarisi
            {
                KullaniciId = kullaniciId,
                YarisId = Id,
            };
            _context.KullaniciYarisi.Add(kullaniciYaris);
            _context.SaveChanges();
            return RedirectToAction("Index","Yaris");
        }
    }
}
