using EmailSendingFunctionApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EmailSendingFunctionApp.Services
{
    public class TemplateStoreServices : ITemplateStoreService
    {
        private readonly string ConnectionString;
        private readonly string ContainerName;

        public TemplateStoreServices(string connectionString, string container)
        {
            ConnectionString = connectionString;
            ContainerName = container;
        }
        public string GetTemplate(string name)
        {
            return GetBlob(name);
        }

        public string GetBlob(string fileName)
        {                        
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);            
            CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();            
            CloudBlobContainer container = serviceClient.GetContainerReference($"{ContainerName}");            
            CloudBlockBlob blob = container.GetBlockBlobReference($"{fileName}");            
            string contents = blob.DownloadTextAsync().Result;
            return contents;
        }
    }
}
