using Dapper;
using Microsoft.Data.SqlClient;
using TurfTime.Web.Models;

namespace TurfTime.Web.Repositories
{
    public class TurfRepository : ITurfRepository
    {
        private readonly string _cs;

        public TurfRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")!;
        }

        public List<Turf> GetAll()
        {
            using var conn = new SqlConnection(_cs);
            return conn.Query<Turf>("SELECT * FROM Turfs").ToList();
        }

        public Turf? GetById(int id)
        {
            using var conn = new SqlConnection(_cs);
            return conn.QueryFirstOrDefault<Turf>("SELECT * FROM Turfs WHERE Id=@id", new { id });
        }

        public List<Turf> Search(string? sport, string? city)
        {
            using var conn = new SqlConnection(_cs);
            return conn.Query<Turf>(
                @"SELECT * FROM Turfs
                  WHERE (@sport IS NULL OR Sport = @sport)
                    AND (@city IS NULL OR City = @city)",
                new { sport, city }).ToList();
        }

        public void Add(Turf t)
        {
            using var conn = new SqlConnection(_cs);
            conn.Execute(
                "INSERT INTO Turfs (Name, Sport, City, PricePerHour) VALUES (@Name, @Sport, @City, @PricePerHour)", t);
        }

        public void Update(Turf t)
        {
            using var conn = new SqlConnection(_cs);
            conn.Execute(
                "UPDATE Turfs SET Name=@Name, Sport=@Sport, City=@City, PricePerHour=@PricePerHour WHERE Id=@Id", t);
        }

        public void Delete(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Execute("DELETE FROM Turfs WHERE Id=@id", new { id });
        }
    }
}