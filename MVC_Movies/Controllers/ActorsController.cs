using Microsoft.AspNetCore.Mvc;
using MVC_Movies.Data;
using MVC_Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Controllers
{
    public class ActorsController : Controller
    {
        private readonly RepositoryContext _repositoryContext;

        public ActorsController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IActionResult Index()
        {
            var actors = _repositoryContext.Actor.ToList();
            return View(actors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }


            _repositoryContext.Actor.Add(actor);
            _repositoryContext.SaveChanges();

            ViewData["Response"] = $"Actor {actor.Name} added successfully";
            ViewData["Error"] = "Failed";
            return View();
        }

        public IActionResult Update(int id)
        {
            var dbActor = _repositoryContext.Actor.Find(id);

            if (dbActor == null)
                return NotFound();

            return View(dbActor);
        }

        [HttpPost]
        public IActionResult Update(Actor actor)
        {
            var dbActor = _repositoryContext.Actor.Find(actor.ID);

            if (dbActor == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            dbActor.Name = actor.Name;
            dbActor.BirthDate = actor.BirthDate;
            dbActor.OscarWon = actor.OscarWon;

            _repositoryContext.SaveChanges();

            ViewData["Response"] = $"Actor {actor.Name} updated successfully";
            ViewData["Error"] = "Failed";


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var dbActor = _repositoryContext.Actor.Find(id);

            if (dbActor == null)
                return NotFound();

            return View(dbActor);
        }

        [HttpPost]
        public IActionResult Delete(Actor actor)
        {
            var dbActor = _repositoryContext.Actor.Find(actor.ID);

            if (dbActor == null)
                return NotFound();

            _repositoryContext.Actor.Remove(dbActor);
            _repositoryContext.SaveChanges();

            ViewData["Response"] = $"Actor {actor.Name} deleted successfully";
            ViewData["Error"] = "Failed";

            return RedirectToAction(nameof(Index));
        }
    }
}
