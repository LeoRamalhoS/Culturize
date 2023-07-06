using Azure.Storage.Blobs;

namespace CulturizeWeb.Services
{
    public interface IBlobStorageService
    {
        Task UploadImageAsync(IFormFile formFile, string blobName);

        BlobClient GetBlob(string blobName);

        Task DeleteBlobAsync(string blobName);
    }
}
