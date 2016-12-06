using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ProyectoMoya.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProyectoMoya.Repositories
{
    public class AzureImageStorageContainer : IImageStorageContainer
    {
        private string connectionstring;

        public AzureImageStorageContainer(string connection)
        {
            connectionstring = connection;
        }

        public string GuardarImagen(string contenedor, string nombre, Stream archivo)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionstring);
            // Create the table client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Intentar crear contenedor
            CloudBlobContainer container = blobClient.GetContainerReference(contenedor.ToLower().Trim());
            //Crear contenedor en caso de que no exista
            container.CreateIfNotExists();
            //Poner permisos al contenedor
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            //Guardar archivo
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombre.ToLower().Trim());

            // Create or overwrite the "myblob" blob with contents from a local file.
            blockBlob.UploadFromStream(archivo);

            return blockBlob.Uri.ToString();

        }

        public string LeerImagen(string contenedor, string nombre)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionstring);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(contenedor.ToLower().Trim());
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombre.ToLower().Trim());

            return blockBlob.Uri.ToString();
        }
    }
}