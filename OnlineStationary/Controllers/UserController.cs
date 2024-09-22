using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineStationary.DTOs.UserDto;
using OnlineStationary.Interfaces.IServices;
using System.Security.Claims;

namespace OnlineStationary.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel loginRequest)
        {
            var user = _userService.Login(loginRequest);
            if (!user.IsSucessful)
            {
                TempData["error"] = user.Message;
                return View(loginRequest);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Data.Email),
                new Claim(ClaimTypes.Name, user.Data.Username),
            };

            foreach(var role in user.Data.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme ,principal, properties);

            if(user.Data.UserRoles.Select(x => x.Name).Contains("Admin"))
            {
                return RedirectToAction("GetAllAuthor", "Author");
            }
            if(user.Data.UserRoles.Select(x => x.Name).Contains("Author"))
            {
                TempData["sucess"] = user.Message;
                return RedirectToAction("CreateBook", "Book");
            }

            if(user.Data.UserRoles.Select(x => x.Name).Contains("Customer"))
            {
                TempData["sucess"] = user.Message;
                return RedirectToAction("GetAllAvailablesBook", "Book");
            }

            return View(loginRequest);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
