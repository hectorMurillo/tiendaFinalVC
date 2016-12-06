using ProyectoMoya.Interfaces;
using ProyectoMoya.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMoya.Controllers
{
    public class HomeController : Controller
    {

        private IProductsInterface productos;
        private ICategoriesInterface Categories;

        public HomeController(IProductsInterface prods, ICategoriesInterface cats)
        {
            productos = prods;
            Categories = cats;
            ViewBag.Categories = cats.getCategories();
        }

        public ActionResult Index()
        {
            var products = productos.getProducts().Select(x => new ProductoEnIndiceModel()
                {
                    ID = x.ID,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Costo = x.Costo,
                    Imagen = x.Imagen,
                    Categoria = x.Categoria
            }).ToList();

            var Categoriesx = Categories.getCategories().Select(x => new CategoriaEnIndiceModel()
            {
                ID = x.ID,
                Nombre = x.Nombre,
            }).ToList();

            //ViewBag.Categories = Categoriesx;
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }


    public class ProductoEnIndiceModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
    }

    public class CategoriaEnIndiceModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
    }
}