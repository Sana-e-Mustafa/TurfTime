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
            var turfs = _repo.GetAll();
            return View(turfs);
        }
    }
}