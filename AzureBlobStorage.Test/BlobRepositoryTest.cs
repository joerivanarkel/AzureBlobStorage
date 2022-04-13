using AutoFixture;
using AzureBlobStorage;
using Azure.Storage.Blobs;
using Xunit;
using System.IO;
using System;

namespace AzureBlobStorage.Test;

public class BlobRepositoryTest
{
    [Fact]
    public async void ShouldWriteToBlob()
    {
        BlobRepository blobRepository = new BlobRepository();
        string localPath = "./data/";
        string fileName = "quickstart " + Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");
        var result = await blobRepository.WriteToBlobContainer(fileName, localFilePath);
        Assert.True(result);
    }

    [Fact]
    public async void ShouldGetBlobFromContainer()
    {
        BlobRepository blobRepository = new BlobRepository();
        string localPath = "./data/";
        string fileName = "quickstart " + Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");
        await blobRepository.WriteToBlobContainer(fileName, localFilePath);
        var result = await blobRepository.GetBlobFromContainer(localFilePath, fileName);
        Assert.True(result);
    }

    [Fact]
    public async void ShouldGetAllBlobsFromContainer()
    {
        BlobRepository blobRepository = new BlobRepository();
        string localPath = "./data/";
        string fileName = "quickstart " + Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");
        await blobRepository.WriteToBlobContainer(fileName, localFilePath);
        var result = await blobRepository.GetAllFromContainer(localPath);
        Assert.True(result);
    }

    [Fact]
    public async void ShouldDeleteBlobContainer()
    {
        BlobRepository blobRepository = new BlobRepository();
        string localPath = "./data/";
        string fileName = "quickstart " + Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");
        await blobRepository.WriteToBlobContainer(fileName, localFilePath);
        var result = await blobRepository.DeleteBlobContainer(localFilePath, localFilePath.Replace(".txt", " DOWNLOADED.txt"));
        Assert.True(result);
    }
}