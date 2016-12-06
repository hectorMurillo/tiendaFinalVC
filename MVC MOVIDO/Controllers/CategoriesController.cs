using ProyectoMoya.Interfaces;
using ProyectoMoya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMoya.Controllers
{

    public class CategoriesController : Controller
    {

        private ICategoriesInterface Categories;
        
        public CategoriesController(ICategoriesInterface cats)
        {
            Categories = cats;
            ViewBag.Categories = cats.getCategories();
        }

        [Authorize(Roles = "superAdmin")]
        public ActionResult Index()
        {
            var model = Categories.getCategories();
            return View(model);
        }

        // GET: Categories/Create
        [Authorize(Roles = "superAdmin")]
        public ActionResult Create()
        {
            var model = new Category();
            return View(model);
        }

        // POST: Categories/Create
        [HttpPost]
        [Authorize(Roles = "superAdmin")]
        public ActionResult Create(Category categoriaACrear)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(categoriaACrear);
                }
                Categories.Create(categoriaACrear);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public List<Category> getCategories()
        {
            var Categoriesx = Categories.getCategories();
            return Categoriesx;
        }
    }
}
