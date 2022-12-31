namespace YarisTakip.ViewModel
{
    public class EditKullaniciViewModel
    {
        public string Id { get; set; }
        public int? KosuHizi { get; set; }
        public int? Mesafe { get; set; }
        public string? ProfilResimURL { get; set; }
        public IFormFile Resim { get; set; }
    }
}
