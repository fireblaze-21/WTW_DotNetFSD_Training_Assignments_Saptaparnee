using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class MovieController : Controller
    {
        private static List<Movie> movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", ReleaseYear = 2010, Director = "Christopher Nolan", Rating = 8.8 },
            new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", ReleaseYear = 1972, Director = "Francis Ford Coppola", Rating = 9.2 },
            new Movie { Id = 3, Title = "La La Land", Genre = "Romance", ReleaseYear = 2016, Director = "Damien Chazelle", Rating = 8.0 },
            new Movie { Id = 4, Title = "Mad Max: Fury Road", Genre = "Action", ReleaseYear = 2015, Director = "George Miller", Rating = 8.1 },
            new Movie { Id = 5, Title = "The Matrix", Genre = "Sci-Fi", ReleaseYear = 1999, Director = "Wachowski Siblings", Rating = 8.7 }
        };

        public IActionResult Index()
        {
            return View(movies);
        }
        public IActionResult ByGenre(string genre)
        {
            var filtered = string.IsNullOrEmpty(genre)
                ? movies
                : movies.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.SelectedGenre = genre;
            return View(filtered);
        }

        public IActionResult Search(string query)
        {
            var result = string.IsNullOrWhiteSpace(query)
                ? movies
                : movies.Where(m => m.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Query = query;
            return View(result);
        }

        public IActionResult Details(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        public IActionResult RecentReleases()
        {
            int currentYear = DateTime.Now.Year;
            var recent = movies.Where(m => m.ReleaseYear >= currentYear - 10).ToList();
            return View(recent);
        }

    }
}
// File: Controllers/MovieController.cs