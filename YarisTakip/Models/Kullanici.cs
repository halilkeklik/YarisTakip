using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace YarisTakip.Models
{
    public class Kullanici : IdentityUser
    {
        public int? KosuHizi { get; set; }
        public int? Mesafe { get; set; }
        public Adres? Adres { get; set; }
        public ICollection<Yaris> Yaris { get; set; }
    }
}
