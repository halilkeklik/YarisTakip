using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YarisTakip.Models;

namespace YarisTakip.Data
{
    public class AppDbContext : IdentityDbContext<Kullanici>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Yaris> Yarislar { get; set; }
        public DbSet<Adres> Adresler { get; set; }
    }
}
