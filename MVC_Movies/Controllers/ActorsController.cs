using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;

        public ActorsController(IActorRepository actorRepository, IMovieRepository movieRepository, IMemoryCache memoryCache)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index(ActorFilters actorFilters)
        {
            List<Actor> actors = new();

            if(!_memoryCache.TryGetValue("actors", out List<Actor> cacheValue))
            {
                cacheValue = await _actorRepository.GetActors(actorFilters);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                _memoryCache.Set("actors", cacheValue, cacheOptions);
            }

            actors = cacheValue;

            var actorViewModel = new ActorViewModel
            {
                actors = actors,
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

            _memoryCache.Remove("actors");

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

            _memoryCache.Remove("actors");

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

            _memoryCache.Remove("actors");

            ViewData["Response"] = $"Actor {actor.Name} deleted successfully";
            ViewData["Error"] = "Failed";

            return RedirectToAction(nameof(Index));
        }
    }
}
