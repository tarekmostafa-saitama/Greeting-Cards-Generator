using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UltimateGreetingCards.Persistence.Context;
using UltimateGreetingCards.Persistence.ViewModels;

namespace UltimateGreetingCards.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CardsDbContext _context;


        public AccountController(SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager, CardsDbContext context)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [Route("Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Account/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {

            if (!_context.Users.Any())
            {
                var user = new IdentityUser() { UserName = "Admin", Email = "admin@gmail.com" };
                var result = await _userManager.CreateAsync(user, "123456");
            }

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Dashboard");
                }

                ModelState.AddModelError("", "محاولة تسجيل دخول غير صالحة.");
            }

            return View(model);
        }

        [Route("Account/Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [Route("Account/ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("Account/ChangePassword")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // ChangePasswordAsync changes the user password
                var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                // The new password did not meet the complexity rules or
                // the current password is incorrect. Add these errors to
                // the ModelState and rerender ChangePassword view
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }

                // Upon successfully changing the password refresh sign-in cookie
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Dashboard", "Dashboard");
            }

            return View(model);
        }
    }
}