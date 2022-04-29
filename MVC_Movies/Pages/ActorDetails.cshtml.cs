using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_Movies.Models;
using MVC_Movies.Repository.Interfaces;

namespace MVC_Movies.Pages
{
    public class ActorDetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public ActorRate Form { get; set; }

        public Actor Actor = new Actor();

        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActorDetailsModel(IMovieRepository movieRepository, IActorRepository actorRepository, UserManager<ApplicationUser> userManager)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Actor = await _actorRepository.GetActorByID(id);

            return Page();
        }
    }
}
