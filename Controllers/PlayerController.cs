using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurfTime.Web.Models;
using TurfTime.Web.Repositories;

namespace TurfTime.Web.Controllers
{
    [Authorize(Roles = "Player")]
    public class PlayerController : Controller
    {
        private readonly ITurfRepository _turfs;
        private readonly IBookingRepository _bookings;

        public PlayerController(ITurfRepository turfs, IBookingRepository bookings)
        {
            _turfs = turfs;
            _bookings = bookings;
        }

        public IActionResult Index()
        {
            ViewBag.City = Request.Cookies["city"];
            return View(_turfs.GetAll());
        }

        public IActionResult Search(string? sport, string? city)
        {
            if (!string.IsNullOrEmpty(city))
                Response.Cookies.Append("city", city, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7) });

            if (string.IsNullOrEmpty(sport)) sport = null;
            if (string.IsNullOrEmpty(city)) city = null;
            return PartialView("_TurfList", _turfs.Search(sport, city));
        }

        [HttpPost]
        public IActionResult Book(int turfId, DateTime date, string slot)
        {
            if (date.Date < DateTime.Today)
                return Json(new { success = false, message = "Pick a valid future date." });

            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var ok = _bookings.Create(new Booking { TurfId = turfId, PlayerId = uid, BookingDate = date.Date, Slot = slot });
            return Json(new { success = ok, message = ok ? "Booked successfully!" : "Slot already taken." });
        }

        public IActionResult MyBookings()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return View(_bookings.GetByPlayer(uid));
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            _bookings.Cancel(id, uid);
            return Json(new { success = true });
        }
    }
}