using TurfTime.Web.Models;

namespace TurfTime.Web.Repositories
{
    public class TurfRepository : ITurfRepository
    {
        public List<Turf> GetAll()
        {
            return new List<Turf>
            {
                new Turf { Id = 1, Name = "Green Arena", Sport = "Football", City = "Lahore", PricePerHour = 4000 },
                new Turf { Id = 2, Name = "Smash Court", Sport = "Badminton", City = "Lahore", PricePerHour = 1500 },
                new Turf { Id = 3, Name = "Pitch Pro", Sport = "Cricket", City = "Karachi", PricePerHour = 5000 }
            };
        }
    }
}