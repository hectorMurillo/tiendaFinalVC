using ProyectoMoya.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMoya.Models;
using System.Globalization;

namespace ProyectoMoya.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsInterface products;
        private ICategoriesInterface categories;
        private IImageStorageContainer imagenes;

        public ProductsController(IProductsInterface prods, ICategoriesInterface cats, IImageStorageContainer imgs)
        {
            categories = cats;
            products = prods;
            imagenes = imgs;
            ViewBag.Categories = categories.getCategories();
            ViewBag.SubCategories = categories.getSubCategories();
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
        [Authorize(Roles = "superAdmin")]
        public ActionResult Create()
        {
            var model = new Product();
            return View(model);
        }

        [Authorize(Roles = "superAdmin")]
        [HttpPost]
        public ActionResult Create(Product productoACrear, HttpPostedFileBase imagen, HttpPostedFileBase imagen2, HttpPostedFileBase imagen3)
        {
            var url = string.Empty;
            var url2 = string.Empty;
            var url3 = string.Empty;

            if (imagen == null || string.IsNullOrWhiteSpace(imagen.FileName))
            {
                ModelState.AddModelError("imagen", "debe de subir un archivo");
            }
            else
            {
                url = imagenes.GuardarImagen("imagenestienda", imagen.FileName, imagen.InputStream);
               /* url2 = imagenes.GuardarImagen("imagenestienda", imagen2.FileName, imagen2.InputStream);
                url3 = imagenes.GuardarImagen("imagenestienda", imagen3.FileName, imagen3.InputStream);*/
            }            
            if (!ModelState.IsValid)
            {
                return View(productoACrear);
            }
            try
            {
                productoACrear.Imagen = url;
                /*productoACrear.Imagen2 = url2;
                productoACrear.Imagen3 = url3;*/
                productoACrear.Nombre = serializeNombre(productoACrear.Nombre);
                productoACrear.Descripcion = serializeNombre(productoACrear.Descripcion);

                products.Create(productoACrear);
                return RedirectToAction("Index");

            }
            catch
            {
                return View(productoACrear);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var model = products.getProduct(id);
            return View(model);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product editar, HttpPostedFileBase imgnueva)
        {
            var model = products.getProduct(id);
            var url = string.Empty;
            var flag = true;

            if ((imgnueva == null || string.IsNullOrEmpty(imgnueva.FileName))&& model.Imagen==null)
            {
                ModelState.AddModelError("imagen", "debe de subir un archivo");
            }
            else if (flag && imgnueva != null)
            {
                url = imagenes.GuardarImagen("imagenestienda", imgnueva.FileName, imgnueva.InputStream);
            }
            else if (flag) { url = model.Imagen; }

            try
            {
                // TODO: Add update logic here
                editar.Imagen = url;
                products.ActualizarImagen(editar);
                products.ActualizarProducto(editar);
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
            var model = products.getProduct(id);
            return View(model);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product editar)
        {
            try
            {
                // TODO: Add delete logic here
                products.BorrarProducto(products.getProduct(id));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public string serializeNombre(string cadena)
        {
            cadena = cadena.ToLower();
            string resul = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(cadena);
            return resul;
        }
    }
}
