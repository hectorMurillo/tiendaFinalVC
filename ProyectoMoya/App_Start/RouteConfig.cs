using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoMoya
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Products",                                           // Route name
                url: "Products",                            // URL with parameters
                defaults: new { controller = "Products", action = "Index", }  // Parameter defaults
            );
            // routes.MapRoute(
            //    name: "Products",                                           // Route name
            //    url: "Products/Create",                            // URL with parameters
            //    defaults: new { controller = "Tienda", action = "CrearProducto", }  // Parameter defaults
            //);
            routes.MapRoute(
                name: "ProductsCreate",                                           // Route name
                url: "ProductsCreate",                            // URL with parameters
                defaults: new { controller = "Products", action = "Create" }
            );

            routes.MapRoute(
                name: "ProductDetails",                                           // Route name
                url: "ProductDetails/{id}",                            // URL with parameters
                defaults: new { controller = "Products", action = "ProductDetails", id = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                name: "FilterByCategoria",                                           // Route name
                url: "categoria/{categoria}/{subCategoria}",                            // URL with parameters
                defaults: new { controller = "Tienda", action = "FilterByCategoria", subCategoria = UrlParameter.Optional, categoria = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                name: "ListaCategories",                                           // Route name
                url: "Categories",                            // URL with parameters
                defaults: new { controller = "Tienda", action = "Categories" }
            );
            routes.MapRoute(
               name: "CategoriesIndex",                                           // Route name
               url: "Categories/Index",                            // URL with parameters
               defaults: new { controller = "Categories", action = "Index" }
           );

            routes.MapRoute(
                name: "CategoriesCreate",                                           // Route name
                url: "Categories/Create",                            // URL with parameters
                defaults: new { controller = "Categories", action = "Create" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Tienda", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Admin",                                           // Route name
               url: "Admin",                            // URL with parameters
               defaults: new { controller = "Admin", action = "Index" }
           );

        }
    }
}
