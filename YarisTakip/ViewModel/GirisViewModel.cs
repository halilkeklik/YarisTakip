using System.ComponentModel.DataAnnotations;

namespace YarisTakip.ViewModel
{
    public class GirisViewModel
    {
        [Required(ErrorMessage = "Email adresi gereklidir")]
        public string EmailAdres { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}
