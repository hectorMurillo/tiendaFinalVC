using ProyectoMoya.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoMoya.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Data.Services.Client;

namespace ProyectoMoya.Repositories
{
    public class AzureProductsRepository : IProductsInterface
    {

        private string connectionstring;

        public AzureProductsRepository(string connection)
        {
            connectionstring = connection;
        }

        public void ActualizarImagen(Product editar)
        {
            var table = GetTableReference();
            var query = TableOperation.Retrieve<ProductoEntity>("Productos", editar.ID.ToString());
            var producto = table.Execute(query);

            if (producto?.Result != null)
            {
                var p = (ProductoEntity)producto.Result;

                TableOperation updateOperation = TableOperation.Replace(p);
                p.Imagen = editar.Imagen;

                table.Execute(updateOperation);
            }
        }

        public void ActualizarProducto(Product editar)
        {
            var table = GetTableReference();
            var query = TableOperation.Retrieve<ProductoEntity>("Productos", editar.ID.ToString());
            var producto = table.Execute(query);

            if (producto?.Result != null)
            {
                var p = (ProductoEntity)producto.Result;

                TableOperation updateOperation = TableOperation.Replace(p);
                p.Nombre = editar.Nombre;
                p.Costo = editar.Costo;
                p.Descripcion = editar.Descripcion;
                p.Categoria = editar.Categoria;
                p.Subcategoria = editar.Subcategoria;
                table.Execute(updateOperation);
            }
        }

        public void BorrarProducto(Product productoABorrar)
        {
            var table = GetTableReference();
            var query = TableOperation.Retrieve<ProductoEntity>("Productos", productoABorrar.ID.ToString());
            var producto = table.Execute(query);
            if (producto?.Result != null)
            {
                var p = (ProductoEntity)producto.Result;
                TableOperation updateOperation = TableOperation.Delete(p);
                table.Execute(updateOperation);
            }
        }

        public void Create(Product nuevo)
        {
            var table = GetTableReference();
            table.CreateIfNotExists();


            var nextId = int.Parse((getProducts().Max(p => p.ID).ToString()));
            ProductoEntity entity = new ProductoEntity((nextId + 1).ToString());
            entity.Imagen = nuevo.Imagen;
            entity.Nombre = nuevo.Nombre;
            entity.Categoria = nuevo.Categoria;
            entity.Costo = nuevo.Costo;
            entity.Descripcion = nuevo.Descripcion;
            entity.Subcategoria = nuevo.Categoria+"."+nuevo.Subcategoria;

            TableOperation insertOperation = TableOperation.Insert(entity);
            table.Execute(insertOperation);

        }

        public Product getProduct(int id)
        {
            var table = GetTableReference();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            var query = TableOperation.Retrieve<ProductoEntity>("Productos", id.ToString());

            var producto = table.Execute(query);

            if (producto?.Result != null)
            {
                var e = (ProductoEntity)producto.Result;
                return new Models.Product()
                {
                    ID = int.Parse(e.ID),
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion,
                    Costo = e.Costo,
                    Imagen = e.Imagen,
                    Categoria = e.Categoria,
                    Subcategoria = e.Subcategoria
                };
            }
            else
            {
                return null;
            }
        }

        public List<Product> getProducts()
        {
            var table = GetTableReference();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<ProductoEntity> query = new TableQuery<ProductoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Productos"));


            var productos = table.ExecuteQuery(query);

            if (productos != null)
            {
                return productos.Select(e => new Product()
                {
                    ID = int.Parse(e.ID),
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion,
                    Costo = e.Costo,
                    Imagen = e.Imagen,
                    Categoria = e.Categoria,
                    Subcategoria = e.Subcategoria
                }).ToList();
            }
            else
            {
                return null;
            }
        }

        private CloudTable GetTableReference()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionstring);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Create the CloudTable object that represents the "productos" table.
            CloudTable table = tableClient.GetTableReference("productos");

            return table;
        }

        private CloudTable GetTableReferenceCategories()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionstring);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Create the CloudTable object that represents the "productos" table.
            CloudTable table = tableClient.GetTableReference("Categories");

            return table;
        }

    }


    public class ProductoEntity : TableEntity
    {
        public ProductoEntity(string id)
        {
            this.PartitionKey = "Productos";
            this.ID = id;
            this.RowKey = id;
        }

        public ProductoEntity()
        {

        }

        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
    }
}