using System.ComponentModel.DataAnnotations;

namespace YarisTakip.ViewModel
{
    public class KayitViewModel
    {
        [Display(Name = "Email Adresi")]
        [Required(ErrorMessage = "Email adresi gereklidir")]
        public string EmailAdres { get; set; }
        [Display(Name = "Şifre")]
        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
        [Display(Name = "Şifreyi Doğrula")]
        [Required(ErrorMessage = "Şifreyi doğrulamak zorunludur!")]
        [DataType(DataType.Password)]
        [Compare("Sifre", ErrorMessage = "Sifre Uyumlu Değil!")]
        public string SifreOnayla { get; set; }
    }
}
