using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using YarisTakip.Data;
using YarisTakip.Interfaces;
using YarisTakip.Models;

namespace YarisTakip.Repository
{
    public class YarisRepository: IYarisRepository
    {
        private readonly AppDbContext _context;

        public YarisRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Yaris yaris)
        {
            _context.Add(yaris);
            return Save();
        }

        public bool Delete(Yaris yaris)
        {
            _context.Remove(yaris);
            return Save();
        }

        public async Task<IEnumerable<Yaris>> GetAll()
        {
            return await _context.Yarislar.ToListAsync();
        }

        public async Task<IEnumerable<Yaris>> GetAllYarisBySehir(string sehir)
        {
            return await _context.Yarislar.Where(c => c.Adres.Sehir.Contains(sehir)).ToListAsync();
        }

        public async Task<Yaris> GetByIdAsync(int id)
        {
            return await _context.Yarislar.Include(i => i.Adres).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Yaris> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Yarislar.Include(i => i.Adres).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Yaris yaris)
        {
            _context.Update(yaris);
            return Save();
        }
    }
}
