﻿using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel p)
        {
            AppUser appUser = new AppUser()
            {
                Name = p.Name,
                Surname = p.Surname,
                UserName = p.UserName,
                Email = p.Email,
            };

            var existingUser = await _userManager.FindByNameAsync(p.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "This username is already exist.");
                return View();
            }

            existingUser = await _userManager.FindByEmailAsync(p.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "This email is already exist.");
                return View();
            }

            existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Name == p.Name && u.Surname == p.Surname);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "This Name and Surname is already exist.");
                return View();
            }

            if (p.Password == p.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, p.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }


        public IActionResult Login()
        {
            return View();
        }
    }
}
