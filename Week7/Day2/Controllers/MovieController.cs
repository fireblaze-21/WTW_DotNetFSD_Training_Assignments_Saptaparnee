using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.ToListAsync();
            return View(movies);
        }

        // GET: Movie/ByGenre?genre=Action
        public async Task<IActionResult> ByGenre(string genre)
        {
            var filtered = string.IsNullOrEmpty(genre)
                ? await _context.Movies.ToListAsync()
                : await _context.Movies
                    .Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();

            ViewBag.SelectedGenre = genre;
            return View(filtered);
        }

        // GET: Movie/Search?query=inception
        public async Task<IActionResult> Search(string query)
        {
            var result = string.IsNullOrWhiteSpace(query)
                ? await _context.Movies.ToListAsync()
                : await _context.Movies
                    .Where(m => m.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();

            ViewBag.Query = query;
            return View(result);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // GET: Movie/RecentReleases
        public async Task<IActionResult> RecentReleases()
        {
            int currentYear = DateTime.Now.Year;
            var recent = await _context.Movies
                .Where(m => m.ReleaseYear >= currentYear - 10)
                .ToListAsync();

            return View(recent);
        }

        // GET: Movie/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(movie);

            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Movie/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }
    }
}

// File: Controllers/MovieController.cs