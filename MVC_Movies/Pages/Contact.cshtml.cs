using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_Movies.Models;

namespace MVC_Movies.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public ContactRequest Form { get; set; }

        public string Message { get; set; } = "Contact ";

        public void OnGet()
        {
            Message += $"Us";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return Page();
            }

            // Send Email

            return RedirectToAction("Index", "Home");
        }

        //Put name of post after the "OnPost"
        public async Task<IActionResult> OnPostCancelAsync()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
