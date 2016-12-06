using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoMoya.Models
{
    public class CarritoModel
    {
        public CarritoModel()
        {
            Productos = new List<Models.ProductoEnCarritoModel>();
        }
        public List<ProductoEnCarritoModel> Productos { get; private set; }

        public decimal Total
        {
            get
            {
                if (Productos.Count == 0)
                {
                    return 0;
                }
                return Productos.Sum(p => p.Importe);
            }
        }
    }
    public class ProductoEnCarritoModel
    {
        public int Cantidad { get; set; }
        public string productoID { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe
        {
            get
            { return Cantidad * Precio; }
        }
    }
}