using System.Diagnostics;
using YarisTakip.Models;

namespace YarisTakip.Interfaces
{
    public interface IYarisRepository
    {
        Task<IEnumerable<Yaris>> GetAll();
        Task<Yaris> GetByIdAsync(int id);
        Task<IEnumerable<Yaris>> GetAllYarisBySehir(string sehir);
        bool Add(Yaris yaris);
        bool Update(Yaris yaris);
        bool Delete(Yaris yaris);
        bool Save();
    }
}
