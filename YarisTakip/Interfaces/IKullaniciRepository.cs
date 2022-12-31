using YarisTakip.Models;

namespace YarisTakip.Interfaces
{
    public interface IKullaniciRepository
    {
        Task<IEnumerable<Kullanici>> GetAllUsers();
        Task<Kullanici> GetUserById(string id);
        bool Add(Kullanici kullanici);
        bool Update(Kullanici kullanici);
        bool Delete(Kullanici kullanici);
        bool Save();
    }
}
