using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenHelper.Controllers
{
    public class АdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}