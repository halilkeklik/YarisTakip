using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace YarisTakip.Models
{
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }
        public int? KosuHizi { get; set; }
        public int? Mesafe { get; set; }
        public Adres? Adres { get; set; }
        public ICollection<Yaris> Yaris { get; set; }
    }
}
