using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace YarisTakip.Models
{
    public class Yaris
    {
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string? Rasim { get; set; }
        [ForeignKey("Adres")]
        public int AdresId { get; set; }
        public Adres Adres { get; set; }
        public YarisKategori YarisKategori { get; set; }
        [ForeignKey("Kullanici")]
        public int? KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }
    }
}
