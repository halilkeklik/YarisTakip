using System.Diagnostics;
using YarisTakip.Data.Enum;
using YarisTakip.Models;

namespace YarisTakip.Data
{
    public class IlkData
    {
        public static void TohumData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Yarislar
                if (!context.Yarislar.Any())
                {
                    context.Yarislar.AddRange(new List<Yaris>()
                    {
                        new Yaris()
                        {
                            Baslik = "Kosu Yarisi 1",
                            Resim = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Aciklama = "Bu ilk yarışın açıklaması",
                            YarisKategori = YarisKategori.Maraton,
                            Adres = new Adres()
                            {
                                Sokak = "11770 SK.",
                                Sehir = "Mersin",
                                Ulke = "Turkiye"
                            }
                        },
                        new Yaris()
                        {
                            Baslik = "Kosu Yarisi 1",
                            Resim = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Aciklama = "Bu ikinci yarışın açıklaması",
                            YarisKategori = YarisKategori.Ultra,
                            Adres = new Adres()
                            {
                                Sokak = "123 Main St",
                                Sehir = "Charlotte",
                                Ulke = "ABD"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
}
}
