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
    public class TiendaController : Controller
    {
        private IProductsInterface products;
        private ICategoriesInterface categories;
        

        public TiendaController(IProductsInterface prods, ICategoriesInterface cats)
        {
            categories = cats;
            products = prods;
            ViewBag.Categories = categories.getCategories();
            ViewBag.Products = products.getProducts();
        }

        // GET: Tienda Dejar aqui
        public ActionResult Index()
        {
            return View();
        }


        /* Este va a ser el Index en el ProductsController
        public ActionResult ListaProductos()
        {
            return View();
        }*/


        //Dejarlo aqui
        public ActionResult FilterByCategoria(string categoria)
        {
            ViewBag.Categorieseleccionada = categoria;
            var search = products.getProducts()
                .Where(p => p.Categoria == categoria)
                .Select(p => new Product()
                {
                    ID = p.ID,
                    Imagen = p.Imagen,
                    Nombre = p.Nombre,
                    Costo = p.Costo
                }).ToList();
            return View(search);
        }




        /* mover al categoriescontroller
         get: categories/create
        [authorize(roles = "superadmin")]
        public actionresult create()
        {
            var model = new category();
            return view(model);
        }*/


        /* mover al categoriescontroller
         * 
         * 
         * 
        [authorize(roles = "superadmin")]
        public actionresult categories()
        {
            var model = categories.getcategories();
            return view(model);
        }

            
         post: categories/create
        [httppost]
        [authorize(roles = "superadmin")]
        public actionresult create(category categoriaacrear)
        {
            try
            {
                 todo: add insert logic here
                if (!modelstate.isvalid)
                {
                    return view(categoriaacrear);
                }

                categoriaacrear.nombre = serializenombre(categoriaacrear.nombre);
                categories.create(categoriaacrear);
                return redirecttoaction("index");
            }
            catch
            {
                return view();
            }
        }
        */

        /* mover al productcontroller
         * 
        get: productos/create
        [authorize(roles = "superadmin")]
        public actionresult crearproducto()
        {
            var model = new product();
            return view(model);
        }

         post: productos/create
        [authorize(roles = "superadmin")]
        [httppost]
        public actionresult crearproducto(product productoacrear, httppostedfilebase imagen)
        {
            var url = string.empty;

            if (imagen == null || string.isnullorwhitespace(imagen.filename))
            {
                modelstate.addmodelerror("imagen", "debe de subir un archivo");
            }
            else
            {
                url = imagenes.guardarimagen("imagenestienda", imagen.filename, imagen.inputstream);
            }



            if (!modelstate.isvalid)
            {
                return view(productoacrear);
            }
            try
            {
                 todo: add insert logic here
                productoacrear.imagen = url;

                productoacrear.nombre = serializenombre(productoacrear.nombre);
                productoacrear.descripcion = serializenombre(productoacrear.descripcion);

                products.create(productoacrear);
                guardar imagen en algun lugar......
                return view(productoacrear);
                return redirecttoaction("index");
            }
            catch
            {
                return view();
            }
        }

            
        public actionresult productdetails(int id)
        {
            var model = products.getproduct(id);
            return view(model);
        }

        get: productos/edit
        public actionresult actualizarproducto(int id)
        {
            var model = products.getproduct(id);
            return view(model);
        }

        post: producto/edit
        [httppost]
        public actionresult actualizarproducto(int id, product producto)
        {
            try
            {
                products.actualizarproducto(producto);
                return redirecttoaction("index");
            }
            catch
            {
                return view();
            }
        }


        post: producto/borrar
        [httppost]
        public actionresult borrarproducto(int id, product producto)
        {
            try
            {
                products.borrarproducto(producto);
                return redirecttoaction("index");
            }
            catch
            {
                return view();
            }
        }
        
        get: producto/borrar
        public actionresult borrarproducto(int id)
        {
            var model = products.getproduct(id);
            return view(model);
        }
             
             */



        /* mover al admincontroller
        [authorize(roles = "superadmin")]
        public actionresult addadmin()
        {
            var model = usuarios.obtenerusuarios();
            viewbag.roles = usuarios.obtenerroles();
            return view(model);
        }*/


        public string serializeNombre(string cadena)
        {
            cadena = cadena.ToLower();
            string resul = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(cadena);
            return resul;
        }

    }
}