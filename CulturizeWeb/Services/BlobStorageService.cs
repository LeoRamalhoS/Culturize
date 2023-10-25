using Azure.Storage.Blobs;
using System.Reflection.Metadata;

namespace CulturizeWeb.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private const string _containerName = "dados";
        private readonly BlobContainerClient _containerClient;
        private readonly string _blobSasToken;

        public BlobStorageService(IConfiguration configuration)
        {
            string conn = configuration.GetSection("BlobConnection").Value!;
            var blobServiceClient = new BlobServiceClient(conn);
            _containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            _blobSasToken = configuration.GetSection("BlobSASToken").Value!;
        }

        public async Task UploadImageAsync(IFormFile formFile, string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(formFile.OpenReadStream(), overwrite: true);
        }

        public string GetBlobUri(string blobName)
        {
            var client = _containerClient.GetBlobClient(blobName);
            return $"{client.Uri}{_blobSasToken}";
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteAsync();
        }
    }

}
