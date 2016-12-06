using ProyectoMoya.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMoya.Controllers
{
    public class AdminController : Controller
    {
        

        public AdminController(ICategoriesInterface cats)
        {
            ViewBag.Categories = cats.getCategories();
        }

        public AdminController()
        {

        }

        [Authorize(Roles = "superAdmin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
