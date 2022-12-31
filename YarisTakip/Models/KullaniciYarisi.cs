using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace YarisTakip.Models
{
    public class KullaniciYarisi
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Kullanici")]
        public string? KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }
        [ForeignKey("Yaris")]
        public int? YarisId { get; set; }
        public Yaris? Yaris { get; set; }
    }
}
