namespace TurfTime.Web.Models
{
    public class Turf
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Sport { get; set; } = "";
        public string City { get; set; } = "";
        public int PricePerHour { get; set; }
    }
}