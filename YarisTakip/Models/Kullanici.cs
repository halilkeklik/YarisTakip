using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YarisTakip.Models
{
    public class Kullanici : IdentityUser
    {
        public int? KosuHizi { get; set; }
        public int? Mesafe { get; set; }
        [ForeignKey("Adres")]
        public int? AdresId { get; set; }
        public Adres? Adres { get; set; }
        public ICollection<Yaris> Yaris { get; set; }
    }
}
