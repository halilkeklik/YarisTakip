using Microsoft.EntityFrameworkCore;
using YarisTakip.Models;

namespace YarisTakip.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Yaris> Yarislar { get; set; }
        public DbSet<Adres> Adresler { get; set; }
    }
}
