using AutoFixture;
using AutoFixture.AutoMoq;
using Azure.Storage.Blobs;

namespace AzureBlobStorage.Test
{
    public class TestUtility
    {
        public static BlobContainerClient GetMockContainerClient()
        {
            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
            BlobServiceClient blobServiceClient = fixture.Create<BlobServiceClient>();

            return blobServiceClient.GetBlobContainerClient("test");
        }
    }
}