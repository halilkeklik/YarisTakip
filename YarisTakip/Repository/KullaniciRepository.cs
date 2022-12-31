using Microsoft.EntityFrameworkCore;
using YarisTakip.Data;
using YarisTakip.Interfaces;
using YarisTakip.Models;

namespace YarisTakip.Repository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly AppDbContext _context;

        public KullaniciRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Kullanici kullanici)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Kullanici kullanici)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Kullanici>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Kullanici> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Id == id); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Kullanici kullanici)
        {
            _context.Update(kullanici);
            return Save();
        }
    }
}
