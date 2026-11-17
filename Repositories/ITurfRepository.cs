using TurfTime.Web.Models;

namespace TurfTime.Web.Repositories
{
    public interface ITurfRepository
    {
        List<Turf> GetAll();
    }
}