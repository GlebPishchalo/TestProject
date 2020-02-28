using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChildrenHelper.DataBase;
using ChildrenHelper.Services.Contracts.Interfaces;
using ChildrenHelper.Services.Contracts.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChildrenHelper.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        public AccountController(IUserService userServe)
        {
            userService = userServe;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel loginUserModel)
        {
            if (ModelState.IsValid)
            {
                var res =  await userService.Login(loginUserModel);
                if (res)
                {
                    await Authenticate(loginUserModel.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }


            return View(loginUserModel);

        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel registerUserModel)
        {
           var result=await userService.Register(registerUserModel);
            if (result)
            {
                 
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные ");
            }
            if (string.IsNullOrEmpty(registerUserModel.Email))
            {
                ModelState.AddModelError("Email", "Некорректный Email");
            }
            return View(registerUserModel);
        }
        //private DataBaseContext db;
        //public AccountController(DataBaseContext context)
        //{
        //    db = context;
        //}
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]

        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginUserModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await db.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
        //        if (user != null)
        //        {
        //            await Authenticate(model.Username); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterUserModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        //        if (user == null)
        //        {
        //            // добавляем пользователя в бд
        //            db.Users.Add(new User { Email = model.Email, Username = model.Username, Password = model.Password, Name = model.Name, Surname = model.Surname, SecName = model.SecName });
        //            await db.SaveChangesAsync();

        //            await Authenticate(model.Email); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}
        private async Task Authenticate(string user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user),
             //  new Claim(ClaimsIdentity.DefaultRoleClaimType, u)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
