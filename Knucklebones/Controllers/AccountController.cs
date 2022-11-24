using Microsoft.AspNetCore.Mvc;
using Knucklebones.ViewModel; 
using Knucklebones.Models;
using Microsoft.EntityFrameworkCore;
using static Knucklebones.ViewModel.UserDTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Knucklebones.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Name == model.Name);
                if (user == null && model.Password == model.ConfirmPassword)
                {
                    // добавляем пользователя в бд
                    user = new User { Name = model.Name, Password = model.Password };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user); 

                    return RedirectToAction("Home", "Home");
                }
                else
                    ModelState.AddModelError("", "Такой пользователь уже существует");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Home", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            ViewBag.message = "error";
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim("Password", user.Password)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                "Password");
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
