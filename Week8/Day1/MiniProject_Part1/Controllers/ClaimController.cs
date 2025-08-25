using Microsoft.AspNetCore.Mvc;
using MvcApplication.Models;
using MvcApplication.Services;

namespace MvcApplication.Controllers
{
    public class ClaimController : Controller
    {
        private readonly IClaimService _service;

        public ClaimController(IClaimService service)
        {
            _service = service;
        }

        private void PopulateDropdowns()
        {
            ViewBag.ClaimTypes = new List<string> { "Health", "Auto", "Property", "Life" };
            ViewBag.Statuses = new List<string> { "New", "In Review", "Approved", "Rejected" };
        }

        public IActionResult Index()
        {
            var claims = _service.GetClaims();
            return View(claims);
        }

        public IActionResult Details(int id)
        {
            var claim = _service.GetClaimById(id);
            if (claim == null)
            {
                ViewBag.ErrorMessage = "Claim not found.";
                return View();
            }
            return View(claim);
        }

        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                _service.CreateClaim(claim);
                return RedirectToAction("Index");
            }

            ViewBag.Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            PopulateDropdowns();
            return View(claim);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var claim = _service.GetClaimById(id);
            if (claim == null)
            {
                ViewBag.ErrorMessage = "Claim not found.";
                return View();
            }
            PopulateDropdowns();
            return View(claim);
        }

        [HttpPost]
        public IActionResult Edit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateClaim(claim);
                return RedirectToAction("Index");
            }

            PopulateDropdowns();
            return View(claim);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var claim = _service.GetClaimById(id);
            if (claim == null)
            {
                ViewBag.ErrorMessage = "Claim not found.";
                return View();
            }
            return View(claim);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _service.DeleteClaim(id);
            return RedirectToAction("Index");
        }
    }
}
