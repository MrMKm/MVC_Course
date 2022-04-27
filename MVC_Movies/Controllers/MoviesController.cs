using Microsoft.AspNetCore.Mvc;
using MVC_Movies.Data;
using MVC_Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly RepositoryContext _repositoryContext;

        public MoviesController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IActionResult Index()
        {
            var movies = _repositoryContext.Movie.ToList();
            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            _repositoryContext.Movie.Add(movie);
            _repositoryContext.SaveChanges();

            ViewData["Response"] = $"Movie '{movie.Title}' added successfully";
            ViewData["Error"] = "Failed";
            return View();
        }

        public IActionResult Update(int id)
        {
            var dbmovie = _repositoryContext.Movie.Find(id);

            if (dbmovie == null)
                return NotFound();

            return View(dbmovie);
        }

        [HttpPost]
        public IActionResult Update(Movie movie)
        {
            var dbmovie = _repositoryContext.Movie.Find(movie.ID);

            if (dbmovie == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            dbmovie.Title = movie.Title;
            dbmovie.ReleaseDate = movie.ReleaseDate;
            dbmovie.Price = movie.Price;
            dbmovie.Gender = movie.Gender;
            dbmovie.Rating = movie.Rating;

            _repositoryContext.SaveChanges();

            ViewData["Response"] = $"Movie '{movie.Title}' updated successfully";
            ViewData["Error"] = "Failed";


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var dbMovie = _repositoryContext.Movie.Find(id);

            if (dbMovie == null)
                return NotFound();

            return View(dbMovie);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            var dbMovie = _repositoryContext.Movie.Find(movie.ID);

            if (dbMovie == null)
                return NotFound();

            _repositoryContext.Movie.Remove(dbMovie);
            _repositoryContext.SaveChanges();

            ViewData["Response"] = $"Movie {movie.Title} deleted successfully";
            ViewData["Error"] = "Failed";

            return RedirectToAction(nameof(Index));
        }
    }
}
