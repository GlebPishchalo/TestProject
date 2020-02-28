using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChildrenHelper.DataBase;
using Microsoft.AspNetCore.Mvc;
using ChildrenHelper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using ChildrenHelper.Services.Contracts.Interfaces;

namespace ChildrenHelper.Controllers
{
    public class HomeController : Controller
    {
        readonly DataBaseContext db;
        private readonly IChildrenService childrenService;

        public HomeController(DataBaseContext context, IChildrenService childrenServe)
        {
            db = context;
            childrenService = childrenServe;
            
        }

        //[Authorize(Roles = "admin, user")]
        //[HttpGet]
        //public IActionResult аdmin()
        //{
        //    return View();

        //}

        public async Task<IActionResult> Index(int? diagnoz, string name, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {

            int pageSize = 2;

            //фильтрация
            IQueryable<Children> childrens = db.Childrens.Include(x => x.Diagnoz);

            if (diagnoz != null && diagnoz != 0)
            {
                childrens = childrens.Where(p => p.DiagnozID == diagnoz);
            }
            if (!String.IsNullOrEmpty(name))
            {
                childrens = childrens.Where(p => p.Chname.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    childrens = childrens.OrderByDescending(s => s.Chname);
                    break;
               
                default:
                    childrens = childrens.OrderBy(s => s.Chname);
                    break;
            }

            // пагинация
            var count = await childrens.CountAsync();
            var items = await childrens.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Diagnozes.ToList(), diagnoz, name),
                Childrens = items
                
            };
           

            return View(viewModel);
        }

        //public IActionResult Search(string searchQuery)
        //{
        //    if (string.IsNullOrWhiteSpace(searchQuery) || string.IsNullOrEmpty(searchQuery))
        //    {
        //        return RedirectToAction("Index");
        //    }

        //   // var searchedChildrens = childrenService.GetFilteredChildrens(searchQuery);                                   
        // //   var model = _mapper.FoodsToHomeIndexModel(searchedFoods);

        //    return View();
        //}



    }
}
