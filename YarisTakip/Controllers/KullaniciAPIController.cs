using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarisTakip.Data;
using YarisTakip.Interfaces;
using YarisTakip.Models;
using YarisTakip.Repository;

namespace YarisTakip.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/Kullanici")]
    [ApiController]
    public class KullaniciAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciAPIController(AppDbContext appDbContext, IKullaniciRepository kullaniciRepository)
        {
            _context = appDbContext;
            _kullaniciRepository = kullaniciRepository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Sil(string id)
        {
            var kullanici = await _kullaniciRepository.GetUserById(id);
            _context.Remove(kullanici);
            _context.SaveChanges();
            return Ok();
        }
    }
}
