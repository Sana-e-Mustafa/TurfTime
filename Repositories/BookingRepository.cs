using Dapper;
using Microsoft.Data.SqlClient;
using TurfTime.Web.Models;

namespace TurfTime.Web.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _cs;

        public BookingRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")!;
        }

        public bool Create(Booking b)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            using var tx = conn.BeginTransaction();
            try
            {
                var taken = conn.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM Bookings WHERE TurfId=@TurfId AND BookingDate=@BookingDate AND Slot=@Slot",
                    b, tx);
                if (taken > 0) { tx.Rollback(); return false; }

                conn.Execute(
                    "INSERT INTO Bookings (TurfId, PlayerId, BookingDate, Slot, Status) VALUES (@TurfId, @PlayerId, @BookingDate, @Slot, 'Confirmed')",
                    b, tx);
                tx.Commit();
                return true;
            }
            catch
            {
                tx.Rollback();
                return false;
            }
        }

        public List<Booking> GetByPlayer(string playerId)
        {
            using var conn = new SqlConnection(_cs);
            return conn.Query<Booking>(
                @"SELECT b.*, t.Name AS TurfName FROM Bookings b
                  JOIN Turfs t ON t.Id = b.TurfId
                  WHERE b.PlayerId = @playerId ORDER BY b.BookingDate", new { playerId }).ToList();
        }

        public void Cancel(int id, string playerId)
        {
            using var conn = new SqlConnection(_cs);
            conn.Execute("DELETE FROM Bookings WHERE Id=@id AND PlayerId=@playerId", new { id, playerId });
        }
    }
}