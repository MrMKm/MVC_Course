using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ActorsController : Controller
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMovieRepository _movieRepository;

        public ActorsController(IActorRepository actorRepository, IMovieRepository movieRepository)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index(ActorFilters actorFilters)
        {
            var actorViewModel = new ActorViewModel
            {
                actors = await _actorRepository.GetActors(actorFilters),
                actorFilters = actorFilters,
                movies = await _movieRepository.GetMovies(new MovieFilters())
            };

            return View(actorViewModel);
        }

        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            await _actorRepository.CreateActor(actor);

            ViewData["Response"] = $"Actor {actor.Name} added successfully";
            ViewData["Error"] = "Failed";
            return View();
        }

        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Update(int id)
        {
            var dbActor = await _actorRepository.GetActorByID(id);

            if (dbActor == null)
                return NotFound();

            return View(dbActor);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public async Task<IActionResult> Update(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            await _actorRepository.UpdateActor(actor);

            ViewData["Response"] = $"Actor {actor.Name} updated successfully";
            ViewData["Error"] = "Failed";


            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int id)
        {
            var dbActor = await _actorRepository.GetActorByID(id);

            if (dbActor == null)
                return NotFound();

            return View(dbActor);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public async Task<IActionResult> Delete(Actor actor)
        {
            await _actorRepository.DeleteActor(actor.ID);

            ViewData["Response"] = $"Actor {actor.Name} deleted successfully";
            ViewData["Error"] = "Failed";

            return RedirectToAction(nameof(Index));
        }
    }
}
