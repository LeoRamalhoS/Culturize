using Azure.Storage.Blobs;

namespace CulturizeWeb.Services
{
    public interface IBlobStorageService
    {
        Task UploadImageAsync(IFormFile formFile, string blobName);

        string GetBlobUri(string blobName);

        Task DeleteBlobAsync(string blobName);
    }
}
