using blog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Controllers
{
    public class PostController : Controller
    {
        private readonly MyDB _myDB;
        private readonly IWebHostEnvironment _hosting;

        public PostController(MyDB myDB, IWebHostEnvironment hosting)
        {
            _myDB = myDB;
            _hosting = hosting;

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormFile titlePhoto, string inputText, string Name, string Theme)
        {

            if (Name != null && Theme != null && titlePhoto != null && inputText != null)
            {

                BloggerModel blogger = await _myDB.Bloggers.FirstOrDefaultAsync(i => i.Mail == User.Identity.Name);

                using (var stream = new FileStream(Path.Combine(_hosting.WebRootPath, @"images\", titlePhoto.FileName), FileMode.Create))
                {
                    titlePhoto.CopyTo(stream);
                }

                PostModel news = new PostModel
                {
                    Name = Name,
                    MainPhoto = "../images/" + titlePhoto.FileName,
                    Text = inputText,
                    Author = blogger.Name,
                    Theme = Theme,
                    Date = DateTime.Now.ToString("MM/dd/yyyy")
                };

                _myDB.Posts.Add(news);
                await _myDB.SaveChangesAsync();

                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Не все поля заполнены");
            }
            return View();
        }
    }
}
