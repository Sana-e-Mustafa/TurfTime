using TurfTime.Web.Models;

namespace TurfTime.Web.Repositories
{
    public interface ITurfRepository
    {
        List<Turf> GetAll();
        Turf? GetById(int id);
        List<Turf> Search(string? sport, string? city);
        void Add(Turf t);
        void Update(Turf t);
        void Delete(int id);
    }
}