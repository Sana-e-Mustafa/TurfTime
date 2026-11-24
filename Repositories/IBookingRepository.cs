using TurfTime.Web.Models;

namespace TurfTime.Web.Repositories
{
    public interface IBookingRepository
    {
        bool Create(Booking b);
        List<Booking> GetByPlayer(string playerId);
        void Cancel(int id, string playerId);
    }
}