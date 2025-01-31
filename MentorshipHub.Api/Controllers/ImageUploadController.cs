using MentorshipHub.Api.ConfigurationToFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MentorshipHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {

        private ImageSettings _imageOptions;

        public ImageUploadController(IOptions<ImageSettings> imageOptions)
        {
            _imageOptions = imageOptions.Value;
        }

        [HttpPost(Name = "UploadImage")]
        public ActionResult<UploadDto> UploadImage(IFormFile file)
        {
            #region ValidationExtensions
            var extension = Path.GetExtension(file.FileName);

            //TODO: extension validation in appsettings.json file this best practice

            var extensions = _imageOptions.AllowedExtensions;

            var isAllowedExtension = extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);

            if (!isAllowedExtension)
            {
                return BadRequest(new UploadDto
                {
                    IsSuccess = false,
                    Message = "Invalid file extension"
                });
            }
            #endregion

            #region ValidationSize

            var MaxSize = _imageOptions.MaxSize;
            var isSizeValid = file.Length >= 0 && file.Length <= MaxSize;
            if (!isSizeValid)
            {
                return BadRequest(new UploadDto
                {
                    IsSuccess = false,
                    Message = "Size is not allowed"
                });
            }
            #endregion

            #region StoreFile
            var fileName = $"{Guid.NewGuid()}{extension}";
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            var path = Path.Combine(imagesPath, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            #endregion

            #region GenerateUrl
            var upload = new UploadDto
            {
                IsSuccess = true,
                Url = $"{Request.Scheme}://{Request.Host}/Images/{fileName}"
            };

            return Ok(upload);
            #endregion

        }

    }
}
