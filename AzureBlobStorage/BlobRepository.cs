using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using UserSecrets;

namespace AzureBlobStorage
{
    public class BlobRepository
    {
        private static BlobContainerClient _containerClient;
        public BlobRepository()
        {
            GetBlobServiceClient();
        }
        public static void GetBlobServiceClient()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(UserSecrets<Program>.GetSecret("connectionstring"));
            string containerName = "huhaaaha";
            try
            {
                _containerClient = blobServiceClient.CreateBlobContainer(containerName);
            }
            catch
            {
                _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            }
        }

        public async Task<bool> WriteToBlobContainer(string fileName, string localFilePath)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);
                await blobClient.UploadAsync(localFilePath, true);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBlobContainer(string localFilePath, string downloadFilePath)
        {
            try
            {
                Console.WriteLine("Deleting blob container...");
                await _containerClient.DeleteAsync();
                Console.WriteLine("Deleting the local source and downloaded files...");
                File.Delete(localFilePath);
                File.Delete(downloadFilePath);
                Console.WriteLine("Done");
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> GetBlobFromContainer(string localFilePath, string fileName)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                string downloadFilePath = localFilePath.Replace(".txt", " DOWNLOADED.txt");
                Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);
                await blobClient.DownloadToAsync(downloadFilePath);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> GetAllFromContainer(string localPath)
        {
            try
            {
                await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
                {
                    string localFilePath = Path.Combine(localPath, blobItem.Name);
                    await GetBlobFromContainer(localFilePath, blobItem.Name);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}