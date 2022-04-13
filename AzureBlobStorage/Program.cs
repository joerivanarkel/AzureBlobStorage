using System.Security.AccessControl;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobStorage;

Console.WriteLine("Hello, World!");

BlobRepository blobRepository = new BlobRepository();

// Create and write a new file
string localPath = "./data/";
string fileName = "quickstart " + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localPath, fileName);
await File.WriteAllTextAsync(localFilePath, "Hello, World!");

await blobRepository.WriteToBlobContainer(fileName, localFilePath);

await blobRepository.GetBlobFromContainer(localFilePath, fileName);

Console.WriteLine("Getting all Blobs");
await blobRepository.GetAllFromContainer(localPath);

// Clean up
Console.Write("Press any key to begin clean up");
Console.ReadLine();

await blobRepository.DeleteBlobContainer(localFilePath, localFilePath.Replace(".txt", " DOWNLOADED.txt"));