using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string storageAccount_connectionString = "your azure storage connection string (key)";

            BlobServiceClient blobServiceClient = new BlobServiceClient(storageAccount_connectionString);

            BlobContainerClient b = blobServiceClient.GetBlobContainerClient("<your container name>");

            List<BlobItem> blobItem = b.GetBlobs().ToList();
            
            BlobClient blobClient = b.GetBlobClient(blobItem[2].Name.ToString());

            string localFilePath = @"your local path where you want to download files";
            
            string downloadFilePath = localFilePath + @"\" + blobClient.Name;

            string strDirectoryName = Path.GetDirectoryName(blobClient.Name);
            string strDirectoryFullPath = @"your local path where you want to download files" + @"\" + strDirectoryName;
            
            Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

            // Download the blob's contents and save it to a file
            BlobDownloadInfo download = blobClient.Download();
            if(!Directory.Exists(strDirectoryFullPath))
            {
                Directory.CreateDirectory(strDirectoryFullPath);
            }            
            FileStream downloadFileStream = File.OpenWrite(downloadFilePath);
            download.Content.CopyToAsync(downloadFileStream);
            downloadFileStream.Close();           
        }
    }
}
