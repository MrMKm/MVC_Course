using Microsoft.AspNetCore.Mvc;
using MVC_Movies.Data;
using MVC_Movies.Models;
using MVC_Movies.Models.Filters;
using MVC_Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public MoviesController(IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public async Task<IActionResult> Index(MovieFilters movieFilters)
        {
            var movieVM = new MovieViewModel
            {
                movies = await _movieRepository.GetMovies(movieFilters),
                movieFilters = movieFilters,
                Actors = await _actorRepository.GetActors(new ActorFilters())
            };

            return View(movieVM);
        }

        public async Task<IActionResult> Create()
        {
            return View(new MovieViewModel 
            {
                Actors = await _actorRepository.GetActors(new ActorFilters())
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movieVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            movieVM.Actors = (await _actorRepository.GetActors(new ActorFilters()))
                .Where(a => movieVM.actorsIDs.Contains(a.ID)).ToList();

            await _movieRepository.CreateMovie(movieVM);

            ViewData["Response"] = $"Movie '{movieVM.Title}' added successfully";
            ViewData["Error"] = "Failed";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var dbmovie = await _movieRepository.GetMovieByID(id);

            if (dbmovie == null)
                return NotFound();

            return View(dbmovie);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            if (await _movieRepository.UpdateMovie(movie) == null)
                return NotFound();

            ViewData["Response"] = $"Movie '{movie.Title}' updated successfully";
            ViewData["Error"] = "Failed";


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dbMovie = await _movieRepository.GetMovieByID(id);

            if (dbMovie == null)
                return NotFound();

            return View(dbMovie);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Movie movie)
        {
            await _movieRepository.DeleteMovie(movie);

            ViewData["Response"] = $"Movie {movie.Title} deleted successfully";
            ViewData["Error"] = "Failed";

            return RedirectToAction(nameof(Index));
        }
    }
}
