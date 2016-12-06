using ProyectoMoya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMoya.Interfaces
{
    public interface IProductsInterface
    {
        List<Product> getProducts();
        Product getProduct(int id);
        void Create(Product nuevo);
        void BorrarProducto(Product productoABorrar);
        void ActualizarProducto(Product editar);
        void ActualizarImagen(Product editar);
    }
}
