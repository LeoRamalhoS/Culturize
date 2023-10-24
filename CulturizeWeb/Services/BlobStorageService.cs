using Azure.Storage.Blobs;

namespace CulturizeWeb.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private const string _containerName = "dados";
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            string conn = configuration.GetSection("BlobConnection").Value!;
            var blobServiceClient = new BlobServiceClient(conn);
            _containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
        }

        public async Task UploadImageAsync(IFormFile formFile, string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(formFile.OpenReadStream(), overwrite: true);
        }

        public BlobClient GetBlob(string blobName)
        {
            return _containerClient.GetBlobClient(blobName);
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteAsync();
        }
    }

}
