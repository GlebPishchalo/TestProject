using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildrenHelper.DataBase;
using ChildrenHelper.Models;
using ChildrenHelper.Services;
using ChildrenHelper.Services.Contracts.Interfaces;
using ChildrenHelper.Services.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nancy.Extensions;
using Diagnoz = ChildrenHelper.Models.Diagnoz;

namespace ChildrenHelper.Controllers
{

    public class AdminController : Controller
    {
        private readonly IAdminServices adminService;
      //  public DataBaseContext db;



        public AdminController(DataBaseContext context, IAdminServices adminServe)
        {
            adminService = adminServe;
          //  db = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Children()
        {
            var childrens = adminService.Childrens();


            return View(childrens.ToList());
        }


        public IActionResult Users()
        {
            var users = adminService.Users();
            return View(users.ToList());
        }

        public IActionResult Diagnoz()
        {
            var diagnozs = adminService.Diagnozs();
            return View(diagnozs.ToList());
        }

        public IActionResult Create()
        {
            var diagnozes = adminService.Diagnozs().Select(diagnoz => new DiagnozDropDownModel
            {
                Id = diagnoz.Id,
                Name = diagnoz.Name
            }

            );
            //SelectList diagnozes =
            ViewBag.Diagnozes = new SelectList(diagnozes, "Id", "Name"); ;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChildrenModel childrenModel)
        {

            if (ModelState.IsValid)
            {
                Create();
                var res = await adminService.Create(childrenModel);
                if (res)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверные данные");
                }
            }

            return View(childrenModel);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Такого ребенка не существует");
            }
            Children children = adminService.GetById(id);
            if (id != null)
            {
                Create();
                return View(children);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ChildrenModel children)
        {
            if (ModelState.IsValid)
            {
                var res = await adminService.Delete(children);
                if (res)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Error");
                }
            }

            return View(children);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound("Такого ребенка не существует");
            }
            Children children = adminService.GetById(id);
            if (id != null)
            {
                Create();
                return View(children);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Children children)
        {
            if (ModelState.IsValid)
            {
                var res = await adminService.Edit(children);
                if (res)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Error");
                }
            }

            return View(children);
        }
        public IActionResult Create_user()
        {
            var roles = adminService.Users().Select(role => new RoleDropDownModel
            {
                Id = role.Role.Id,
                Name = role.Role.Name
            }

            );
            //SelectList diagnozes =
            ViewBag.Roles = new SelectList(roles, "Id", "Name"); ;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create_user(RegisterUserModel registeruserModel)
        {

            if (ModelState.IsValid)
            {
                Create_user();
                var res = await adminService.Create_user(registeruserModel);
                if (res)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("", "Неверные данные");
                }
            }
            return View(registeruserModel);

        }
        [HttpGet]
        public IActionResult Edit_user(int? id)
        {

            if (id == null)
            {
                return NotFound("Такого пользователя не существует");
            }
            User user = adminService.GetById_user(id);
            if (id != null)
            {
                Create_user();
                return View(user);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit_user(User user)
        {
            if (ModelState.IsValid)
            {
                var res = await adminService.Edit_user(user);
                if (res)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Error");
                }
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Delete_user(int? id)
        {
            if (id == null)
            {
                return NotFound("Такого пользователя не существует");
            }
            User user = adminService.GetById_user(id);
            if (id != null)
            {
                Create_user();
                return View(user);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete_user(User user)
        {
            if (ModelState.IsValid)
            {
                var res = await adminService.Delete_user(user);
                if (res)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Error");
                }
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Delete_diagnoz(int? id)
        {
            if (id == null)
            {
                return NotFound("Такого диагноза не существует");
            }
            Diagnoz diagnoz = adminService.GetById_diagnoz(id);
            if (id != null)
            {
                
                return View(diagnoz);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete_diagnoz(Diagnoz diagnoz)
        {
            if (ModelState.IsValid)
            {
                var res = await adminService.Delete_diagnoz(diagnoz);
                if (res)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Error");
                }
            }

            return View(diagnoz);
        }
        public IActionResult Edit_diagnoz(int? id)
        {
            if (id == null)
            {
                return NotFound("Такого пользователя не существует");
            }
            Diagnoz diagnoz = adminService.GetById_diagnoz(id);
            if (id != null)
            {
                return View(diagnoz);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit_diagnoz(Diagnoz diagnoz)
        {
            if (ModelState.IsValid)
            {
                var res = await adminService.Edit_diagnoz(diagnoz);
                if (res)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Error");
                }
            }

            return View(diagnoz);
        }
        public IActionResult Create_diagnoz()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create_diagnoz(Diagnoz diagnoz)
        {

            if (ModelState.IsValid)
            {
                
                var res = await adminService.Create_diagnoz(diagnoz);
                if (res)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("", "Неверные данные");
                }
            }
            return View(diagnoz);

        }
        }

    }

