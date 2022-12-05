using Knucklebones.Models;
using Knucklebones.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Knucklebones.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext _context;
        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult PlayerHub()
        {
            ViewBag.user = Newtonsoft.Json.JsonConvert.SerializeObject(User.Identity.Name);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}