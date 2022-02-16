using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Controllers
{
    public class HomeController : Controller
    {

        private readonly MyDB _myDB;

        public HomeController(MyDB myDB)
        {
            _myDB = myDB;
        }
        

        public IActionResult Index()
        {
            ViewBag.Posts = _myDB.Posts.OrderByDescending(p => p.Id);
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
