using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurfTime.Web.Repositories;

namespace TurfTime.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITurfRepository _repo;

        public HomeController(ITurfRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Dashboard");

            return View(_repo.GetAll());
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            if (User.IsInRole("Admin")) return RedirectToAction("Index", "Admin");
            if (User.IsInRole("Owner")) return RedirectToAction("Index", "Owner");
            return RedirectToAction("Index", "Player");
        }
    }
}