using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarisTakip.Data;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.Repository;

namespace YarisTakip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciSilmeAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciSilmeAPIController(AppDbContext appDbContext, IKullaniciRepository kullaniciRepository)
        {
            _context = appDbContext;
            _kullaniciRepository = kullaniciRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Kullanici>>> Get()
        {
            var y = await _context.Kullanicilar.ToListAsync();
            if (y is null)
            {
                return NoContent();
            }
            return y;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanici>> Get(string id)
        {
            var y = await _context.Kullanicilar.FirstOrDefaultAsync(x => x.Id == id);
            if (y is null)
            {
                return NoContent();
            }
            return y;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Kullanici k)
        {
            _context.Kullanicilar.Add(k);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var kullanici = await _kullaniciRepository.GetUserById(id);
            _context.Remove(kullanici);
            _context.SaveChanges();
            return Ok();
        }
    }
}
