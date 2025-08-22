using Microsoft.AspNetCore.Mvc;
using MvcApplication.Data;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class ClaimController : Controller
    {
        private readonly AppDbContext _context;

        public ClaimController(AppDbContext context)
        {
            _context = context;
        }

        private void PopulateDropdowns()
        {
            ViewBag.ClaimTypes = new List<string> { "Health", "Auto", "Property", "Life" };
            ViewBag.Statuses = new List<string> { "New", "In Review", "Approved", "Rejected" };
        }

        // GET: /Claim
        public IActionResult Index()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }
         
        public IActionResult Details(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                ViewBag.ErrorMessage = "Claim not found.";
                return View();
            }
            return View(claim);
        }

        // GET: /Claim/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: /Claim/Create
        [HttpPost]
        public IActionResult Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.CreatedAt = DateTime.UtcNow;
                claim.UpdatedAt = DateTime.UtcNow;

                _context.Claims.Add(claim);
      
                
               _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();
                ViewBag.Errors = errors;
                PopulateDropdowns();
                return View(claim);

                /*ViewBag.ErrorMessage = "Invalid claim details.";
                
                return View();*/
            }
        }

        // GET: /Claim/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                ViewBag.ErrorMessage = "Claim not found.";
                return View();
            }
            PopulateDropdowns();
            return View(claim);
        }

        // POST: /Claim/Edit
        [HttpPost]
        public IActionResult Edit(Claim claim)
        {
            claim.UpdatedAt = DateTime.UtcNow;

            _context.Claims.Update(claim);
            _context.SaveChanges();
            PopulateDropdowns();
            return RedirectToAction("Index");
        }

        // GET: /Claim/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                ViewBag.ErrorMessage = "Claim not found.";
                return View();
            }

            return View(claim);
        }

        // POST: /Claim/Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var claim = _context.Claims.Find(id);

            if (claim != null)
            {
                _context.Claims.Remove(claim);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Requested claim does not exist.";
                return View();
            }
        }
    }
}
