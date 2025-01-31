using MentorshipHub.Api.ConfigurationToFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MentorshipHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadTaskController : ControllerBase
    {
        private TaskSettings taskOptions;
        public UploadTaskController(IOptions<TaskSettings> taskOptions)
        {
            this.taskOptions = taskOptions.Value;
        }

        [HttpPost(Name = "UploadTask")]
        public ActionResult<UploadDto> UploadTask(IFormFile file)
        {
            #region ValidationExtensions
            var extension = Path.GetExtension(file.FileName);

            //TODO: extension validation in appsettings.json file this best practice

            var extensions = taskOptions.AllowedExtensions;

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

            var MaxSize = taskOptions.MaxSize;
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
            var tasksPath = Path.Combine(Directory.GetCurrentDirectory(), "TaskSubmissions");

            if (!Directory.Exists(tasksPath))
            {
                Directory.CreateDirectory(tasksPath);
            }

            var path = Path.Combine(tasksPath, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            #endregion

            #region GenerateUrl
            var upload = new UploadDto
            {
                IsSuccess = true,
                Url = $"{Request.Scheme}://{Request.Host}/TaskSubmissions/{fileName}"
            };

            return Ok(upload);
            #endregion

        }
    }
}
