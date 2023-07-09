using AFwalks.API_7.Models.Domain;
using AFwalks.API_7.Models.DTO;
using AFwalks.API_7.Respositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFwalks.API_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]

        public async Task<IActionResult> Upload ([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);
            if (ModelState.IsValid)
            {
                // Convert DTO to Domain Model
                var imageDomain = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription,
                };

                // use repository to upload image
                imageDomain = await imageRepository.Upload(imageDomain);

                return Ok(imageDomain);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };

            if (!allowedExtension.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file");
            }
        }
    }
}
