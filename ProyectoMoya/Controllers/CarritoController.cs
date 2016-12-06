using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMoya.Models;
namespace ProyectoMoya.Controllers
{
    public class CarritoController : Controller
    {
        public CarritoManager Carrito { get; set; }
        public CarritoController()
        {
            Carrito = new CarritoManager();
        }
        public static int y = 0;
        public string Index()
        {
            //Seciones son parte de ASP        
            
            var x = Session["x"];
            var pageLoads = 0;
            if(x!=null && x is int)
            {
                pageLoads=(int)x;
                pageLoads++;
                Session["x"] = pageLoads;                
            }
            else
            {
                Session["x"] = 1;
            }
            //var model = Carrito.ObtenerCarro();
            return $"X={x}&& Y={y}";
        }
        public ActionResult Agregar(Product p)
        {
            Carrito.Agregar(p);
            return View();
        }
        public ActionResult Quitar(Product p)
        {
            Carrito.Quitar(p);
            return View();
        }
        public ActionResult Checkout()
        {
            return null;
        }
        public ActionResult Vaciar(Product p)
        {
            Carrito.Vaciar();
            return new RedirectResult("Index");
        }

    }    
}
