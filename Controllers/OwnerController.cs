using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurfTime.Web.Models;
using TurfTime.Web.Repositories;

namespace TurfTime.Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class OwnerController : Controller
    {
        private readonly ITurfRepository _repo;

        public OwnerController(ITurfRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index() => View(_repo.GetAll());

        public IActionResult Create() => View("Form", new Turf());

        [HttpPost]
        public IActionResult Create(Turf t)
        {
            if (!ModelState.IsValid) return View("Form", t);
            _repo.Add(t);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var t = _repo.GetById(id);
            if (t == null) return NotFound();
            return View("Form", t);
        }

        [HttpPost]
        public IActionResult Edit(Turf t)
        {
            if (!ModelState.IsValid) return View("Form", t);
            _repo.Update(t);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try { _repo.Delete(id); return Json(new { success = true }); }
            catch { return Json(new { success = false, message = "Cannot delete a turf that has bookings." }); }
        }
    }
}