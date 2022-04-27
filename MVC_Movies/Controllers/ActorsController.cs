using Microsoft.AspNetCore.Mvc;
using MVC_Movies.Data;
using MVC_Movies.Models;
using MVC_Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorRepository _actorRepository;

        public ActorsController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await _actorRepository.GetActors();
            return View(actors);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Update(int id)
        {
            var dbActor = await _actorRepository.GetActorByID(id);

            if (dbActor == null)
                return NotFound();

            return View(dbActor);
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            var dbActor = await _actorRepository.GetActorByID(id);

            if (dbActor == null)
                return NotFound();

            return View(dbActor);
        }

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
