namespace TurfTime.Web.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int TurfId { get; set; }
        public string TurfName { get; set; } = "";
        public string PlayerId { get; set; } = "";
        public DateTime BookingDate { get; set; }
        public string Slot { get; set; } = "";
        public string Status { get; set; } = "Confirmed";
    }
}