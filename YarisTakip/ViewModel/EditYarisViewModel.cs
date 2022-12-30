using YarisTakip.Data.Enum;
using YarisTakip.Models;

namespace YarisTakip.ViewModel
{
    public class EditYarisViewModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public IFormFile Resim { get; set; }
        public string? URL { get; set; }
        public int AdresId { get; set; }
        public Adres Adres { get; set; }
        public YarisKategori YarisKategori { get; set; }
    }
}
