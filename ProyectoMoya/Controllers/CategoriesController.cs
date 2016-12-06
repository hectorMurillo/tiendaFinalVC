using ProyectoMoya.Interfaces;
using ProyectoMoya.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMoya.Controllers
{

    public class CategoriesController : Controller
    {

        private ICategoriesInterface Categories;
        private static List<Category> list = new List<Category>();
         
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
                    categoriaACrear.Nombre = serializeNombre(categoriaACrear.Nombre);
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

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var model = Categories.getCategoryId(id.ToString());
            return View(model);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category editarcat)
        {

            try
            {
                // TODO: Add update logic here
                Categories.updateCategory(editarcat);
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
           
            var model = Categories.getCategoryId(id.ToString());
            return View(model);
        }


        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Category delete)
        {
            try
            {
                Categories.deleteCategory(delete);
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


        public string serializeNombre(string cadena)
        {
            cadena = cadena.ToLower();
            string resul = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(cadena);
            return resul;
        }
    }
}
