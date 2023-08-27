using Azure.Storage.Blobs;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Persistence.Inplementations.Services
{
    public class FileService:IFileService
    {
        private readonly BlobServiceClient _BlobServiceClient;

        public FileService(BlobServiceClient blobServiceClient)
        {
            _BlobServiceClient = blobServiceClient;
        }

        public async Task Upload(FileModels file)
        {
            var ContainerInstance = _BlobServiceClient.GetBlobContainerClient("fiorellaappcontainer");
            var blobInstance = ContainerInstance.GetBlobClient(file.ImageFile.FileName);
            await blobInstance.UploadAsync(file.ImageFile.OpenReadStream());
        }
        public async Task<Stream> Get(string name)
        {
            var ContainerInstance = _BlobServiceClient.GetBlobContainerClient("fiorellaappcontainer");
            var blobInstance = ContainerInstance.GetBlobClient(name);
            var DonwloadContent = await blobInstance.DownloadAsync();
            return DonwloadContent.Value.Content;
        }  
        public async Task<Stream> Download(string name)
        {
            var ContainerInstance = _BlobServiceClient.GetBlobContainerClient("fiorellaappcontainer");
            var blobInstance = ContainerInstance.GetBlobClient(name);
            var DonwloadContent = await blobInstance.DownloadAsync();
            return DonwloadContent.Value.Content;
        }
    }
}
