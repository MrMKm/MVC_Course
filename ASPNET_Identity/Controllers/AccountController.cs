using ASPNET_Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            // Login logic to redirect if credentials are correct 
            var result = await _signInManager.PasswordSignInAsync
                (login.Email, login.Password, login.RememberMe, false);

            if(result.Succeeded)
            {
                return LocalRedirect("/Home/Profile");
            }

            ModelState.AddModelError("", "Invalid login");
            ViewData["Error"] = "User or Password is incorrect";
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest register)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data");
                return View();
            }

            var user = new ApplicationUser(register.Email, register.Name);

            var result = await _userManager.CreateAsync(user, register.Password);

            if(result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, register.Role.ToString());

                if(result.Succeeded)
                    return LocalRedirect("/Account/Login");

                else
                {
                    await _userManager.DeleteAsync(user);
                    ModelState.AddModelError("", "Account couldn't be created");
                }
            }

            foreach(var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewData["Error"] = "User or Password is incorrect";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return LocalRedirect("/");
        }
    }
}
