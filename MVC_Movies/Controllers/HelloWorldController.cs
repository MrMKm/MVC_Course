using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index(string name, int ID)
        {
            ViewData["Title"] = "Hello World";
            ViewData["Name"] = name;
            ViewData["ID"] = ID;

            return View();
        }

        public string Welcome(int ID)
        {
            return $"Welcome to MVC with ID: {ID}";
        }
    }
}
