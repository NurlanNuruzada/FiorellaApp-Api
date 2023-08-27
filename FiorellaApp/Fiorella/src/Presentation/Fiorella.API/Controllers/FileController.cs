using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] FileModels file)
        {
            await _fileService.Upload(file);
            return Ok("success");
        }
        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Get(string FileName)
        {
            var imageFileStream = await _fileService.Get(FileName);
            string FileType = "jpeg";
            if (FileName.Contains("png"))
            {
                FileType = "png";
            }
            return File(imageFileStream, $"image/{FileType}");
        }
        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Download(string FileName)
        {
            var imageFileStream = await _fileService.Get(FileName);
            string FileType = "jpeg";
            if (FileName.Contains("png"))
            {
                FileType = "png";
            }
            return File(imageFileStream, $"image/{FileType}", $"blobfile.{FileType}");
        }
    }
}
