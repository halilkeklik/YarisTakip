using System.ComponentModel.DataAnnotations;

namespace YarisTakip.Models
{
    public class Adres
    {
        [Key]
        public int Id { get; set; }
        public string Sokak { get; set; }
        public string Sehir { get; set; }
        public string Ulke { get; set; }
    }
}
