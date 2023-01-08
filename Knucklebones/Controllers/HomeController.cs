using Knucklebones.Models;
using Knucklebones.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize]
        public IActionResult Game(string opponent, string isgofirst)
        {
            var opp = _context.Users.FirstOrDefault(u => u.Name == opponent);
            var user = _context.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            List<User> participants = new List<User>() {user,opp };
            GameStory gs = new GameStory { Users =  participants};

            _context.GameStories.Add(gs);
            _context.SaveChanges();

            ViewBag.isgofirst = Newtonsoft.Json.JsonConvert.SerializeObject(isgofirst);
            ViewBag.user = Newtonsoft.Json.JsonConvert.SerializeObject(User.Identity.Name);
            ViewBag.opponent = Newtonsoft.Json.JsonConvert.SerializeObject(opponent);
            ViewBag.gameid = Newtonsoft.Json.JsonConvert.SerializeObject(gs.Id);
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