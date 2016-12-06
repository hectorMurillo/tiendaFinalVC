using ProyectoMoya.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMoya.Models;
namespace ProyectoMoya.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsInterface products;
        private ICategoriesInterface categories;


        public ProductsController(IProductsInterface prods, ICategoriesInterface cats)
        {
            categories = cats;
            products = prods;
            ViewBag.Categories = categories.getCategories();
            ViewBag.Products = products.getProducts();
        }
        // GET: Products
        public ActionResult Index()
        {
            var model = ViewBag.Products;
            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult productDetails(int id)
        {
            var model = products.getProduct(id);
            return View(model);
        }

        // GET: Products/Create
        [Authorize(Roles ="superadmin")]
        public ActionResult ProductsCreate()
        {
            var model = new Product();
            return View(model);
        }
        /* [authorize(roles = "superadmin")]
        public actionresult crearproducto()
        {
            var model = new product();
            return view(model);
        }*/

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
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
    }
}
