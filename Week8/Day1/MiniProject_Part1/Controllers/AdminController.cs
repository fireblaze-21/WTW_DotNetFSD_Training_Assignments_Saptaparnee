using Microsoft.AspNetCore.Mvc;
using MvcApplication.Data;
using MvcApplication.Models;
using System.Linq;

namespace MvcApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var totalClaims = _context.Claims.Count();
            var approved = _context.Claims.Count(c => c.Status == "Approved");
            var rejected = _context.Claims.Count(c => c.Status == "Rejected");
            var pending = _context.Claims.Count(c => c.Status == "New" || c.Status == "In Review");

            ViewBag.TotalClaims = totalClaims;
            ViewBag.Approved = approved;
            ViewBag.Rejected = rejected;
            ViewBag.Pending = pending;

            return View();
        }

        // Later: Add filtering, export etc.
    }
}

