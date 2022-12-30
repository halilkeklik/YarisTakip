using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using YarisTakip.Data;
using YarisTakip.Models;

namespace YarisTakip.Controllers
{
    public class YarisController : Controller
    {
        private readonly AppDbContext _context;

        public YarisController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Yaris> yaris = _context.Yarislar.ToList();
            return View(yaris);
        }
        public IActionResult Detail(int id)
        {
            Yaris yaris = _context.Yarislar.Include(a => a.Adres).FirstOrDefault(c => c.Id == id);
            return View(yaris);
        }
    }
}
