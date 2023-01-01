using System.ComponentModel.DataAnnotations;

namespace YarisTakip.ViewModel
{
    public class KayitViewModel
    {
        [Required(ErrorMessage = "Email adresi gereklidir")]
        public string EmailAdres { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
        [Required(ErrorMessage = "Şifreyi doğrulamak zorunludur!")]
        [DataType(DataType.Password)]
        [Compare("Sifre", ErrorMessage = "Sifre Uyumlu Değil!")]
        public string SifreOnayla { get; set; }
    }
}
