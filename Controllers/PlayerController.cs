using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurfTime.Web.Repositories;

namespace TurfTime.Web.Controllers
{
    [Authorize(Roles = "Player")]
    public class PlayerController : Controller
    {
        private readonly ITurfRepository _repo;

        public PlayerController(ITurfRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }
    }
}