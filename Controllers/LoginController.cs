using blog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace blog.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyDB _myDB;

        public LoginController(MyDB myDB) 
        {
            _myDB = myDB;
        }


        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model) 
        {
            if (ModelState.IsValid)
            {
                BloggerModel blogger = await _myDB.Bloggers.FirstOrDefaultAsync(b => b.Mail == model.Mail);
                if (blogger == null)
                {
                    blogger = new BloggerModel
                    {
                        Name = model.Name,
                        Mail = model.Mail,
                        Pass = model.Pass
                    };

                    _myDB.Bloggers.Add(blogger);
                    await _myDB.SaveChangesAsync();

                    await Authenticate(blogger);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Email уже зарегистрирован");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            { 
                BloggerModel blogger = await _myDB.Bloggers.FirstOrDefaultAsync(b => b.Mail == model.Mail);
                if (blogger != null)
                {
                    await Authenticate(blogger);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Логин и пароль не совпадают");
            }
            return View(model);
        }



                public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(BloggerModel model)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, model.Mail),

            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
