using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildrenHelper.DataBase;
using ChildrenHelper.Services.Contracts.Interfaces;
using ChildrenHelper.Services.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChildrenHelper.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly IChildrenService childrenService;
        readonly DataBaseContext db;

        public ChildrenController(IChildrenService childrenServe, DataBaseContext context)
        {
            childrenService = childrenServe;
            db = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            SelectList diagnozes = new SelectList(db.Diagnozes, "Id", "Name");
            ViewBag.Diagnozes = diagnozes;
            return View();
           
        }

        //[HttpGet]
        //public ActionResult Create2()
        //{
        //    var diagnozes = db.Childrens.Include(c => c.Diagnoz);
        //    return View(diagnozes.ToList());
        //}
       
        [HttpPost]
        public async Task<IActionResult> Create(ChildrenModel childrenModel)
        {
            if (ModelState.IsValid)
            {
                SelectList diagnozes = new SelectList(db.Diagnozes, "Id", "Name");
                ViewBag.Diagnozes = diagnozes;
                var res = await childrenService.Create(childrenModel);
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
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChildrenModel childrenModel)
        {
            if (childrenModel.Id != null)
            {
                var res = await childrenService.Edit(childrenModel);
                if (res)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }

            return View(childrenModel);
        }

        [HttpGet]
      

        public IActionResult Delete()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ChildrenModel childrenModel)
        {
            if (childrenModel.Id != null)
            {
                var res = await childrenService.Delete(childrenModel);
                if (res)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return NotFound();



        }
    }
}