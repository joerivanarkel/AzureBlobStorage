[![.NET](https://github.com/joerivanarkel/AzureBlobStorage/actions/workflows/dotnet.yml/badge.svg)](https://github.com/joerivanarkel/AzureBlobStorage/actions/workflows/dotnet.yml)
## Azure Blob Storage
In this example I am creating, fetching and deleting files in a remote Blob Storage container. I have used dotnet secrets to set up a secret connectionstring, in ``DatabaseConnection.cs`` it is converted from secret to string. I am using the Azure.Storage.Blobs NuGet package.

### Setting up Blob Storage
Here we create a new container in a specified storage account. Using this ``containerclient`` we can create, fetch and delete blobs in this container.

```csharp
BlobServiceClient blobServiceClient = new BlobServiceClient("{CONNECTIONSTRING}");
string containerName = "{CONTAINERNAME}";
BlobContainerClient containerClient;
try
{
  containerClient = blobServiceClient.CreateBlobContainer(containerName);
  _containerClient = containerClient;
}
catch
{
  containerClient = blobServiceClient.GetBlobContainerClient(containerName);
  _containerClient = containerClient;
}
```
### Creating a Blob
Firstly we reference the Blobclient object using the ``GetBlobClient()`` method. Using this we can use the ``UploadAsync()`` method to upload this file to the container.

```csharp
BlobClient blobClient = _containerClient.GetBlobClient(fileName);
await blobClient.UploadAsync(localFilePath, true);
```
### Fetching a Blob
To fetch a blob from its container, we once again reference the Blobclient object using the ``GetBlobClient()`` method. Then we modify the ``localFilePath`` to prevent duplication. Finally we us the ``DownloadToAsync()`` method to download the blob. Fetching more than one blob can be done using a ``forearch`` loop.

```csharp
BlobClient blobClient = _containerClient.GetBlobClient(fileName);
string downloadFilePath = localFilePath.Replace(".txt", " DOWNLOADED.txt");
await blobClient.DownloadToAsync(downloadFilePath);
return true
```

### Deleting a Blob
To delete a Blob we can use the ``DeleteBlob()`` method, but in this example we are deleting the entire container. This can both be achieved using the ``containerclient``. Then we ensure that everything is cleaned up, by deleting the files we created locally.

```csharp
await _containerClient.DeleteAsync();
File.Delete(localFilePath);
File.Delete(downloadFilePath);
return true;
```
