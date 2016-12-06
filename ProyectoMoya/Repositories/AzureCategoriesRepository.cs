using ProyectoMoya.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoMoya.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

namespace ProyectoMoya.Repositories
{
    public class AzureCategoriesRepository : ICategoriesInterface
    {

        private string connection;

        public AzureCategoriesRepository(string connection)
        {
            this.connection = connection;
        }

        public void updateCategory(Category editCategory)
        {
            var table = GetTableReference("categorias");
            var query = TableOperation.Retrieve<CategoriaEntity>("Categorias", editCategory.ID.ToString());
            var categories = table.Execute(query);

            if (categories?.Result != null)
            {
                var p = (CategoriaEntity)categories.Result;

                TableOperation updateOperation = TableOperation.Replace(p);
                p.Nombre = editCategory.Nombre;

                // Execute the operation.
                table.Execute(updateOperation);
            }
        }
        public Category getCategoryId(string ID)
        {
            var table = GetTableReference("categorias");
            var query = TableOperation.Retrieve<CategoriaEntity>("Categorias", ID.ToString());

            var categories = table.Execute(query);
            if (categories?.Result != null)
            {
                var cat = (CategoriaEntity)categories.Result;
                return new Models.Category
                {
                    ID = int.Parse(cat.ID),
                    Nombre = cat.Nombre
                };
            }else { 
            return null;
            }
        }
        public void deleteCategory(Category category)
        {
            var table = GetTableReference("Categorias");
            var query = TableOperation.Retrieve<CategoriaEntity>("Categorias",category.ID.ToString());
            var categoria = table.Execute(query);
            if (categoria.Result != null)
            {
                var p = (CategoriaEntity)categoria.Result;
                TableOperation updateOperation = TableOperation.Delete(p);
                table.Execute(updateOperation);
            }
        }

        public void Create(Category newCategory)
        {
            // Retrieve the storage account from the connection string.
            var categories = GetTableReference("categorias");
            //Crear tabla si no existe
            categories.CreateIfNotExists();

            var nextID = this.getCategories().Max(p=> p.ID);

            //Crear entidad
            CategoriaEntity entity = new CategoriaEntity((nextID+1).ToString());
            entity.Nombre = newCategory.Nombre;

            TableOperation insertOperation = TableOperation.Insert(entity);

            categories.Execute(insertOperation);
        }

        public List<Category> getCategories()
        {
            var table = GetTableReference("categorias");

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<CategoriaEntity> query = new TableQuery<CategoriaEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Categorias"));


            var categories = table.ExecuteQuery(query);

            if (categories != null)
            {
                return categories.Select(e => new Category()
                {
                    ID = int.Parse(e.ID),
                    Nombre = e.Nombre
                }).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Category> getSubCategories()
        {
            var table = GetTableReference("subcategorias");

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<CategoriaEntity> query = new TableQuery<CategoriaEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Subcategorias"));


            var categories = table.ExecuteQuery(query);

            if (categories != null)
            {
                return categories.Select(e => new Category()
                {
                    ID = int.Parse(e.ID),
                    Nombre = e.Nombre
                }).ToList();
            }
            else
            {
                return null;
            }
        }

        

        private CloudTable GetTableReference(String type)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference(type);

            return table;
        }

        
    }


    public class CategoriaEntity : TableEntity
    {
        public CategoriaEntity(string id)
        {
            this.PartitionKey = "Categorias";
            this.ID = id;
            this.RowKey = id;
        }

        public CategoriaEntity()
        {

        }

        public string ID { get; set; }
        public string Nombre { get; set; }
    }
}

