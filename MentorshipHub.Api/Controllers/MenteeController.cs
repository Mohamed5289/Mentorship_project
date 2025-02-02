using MentorshipHub.Api.ConfigurationToFile;
using MentorshipHub.Core.DTOHelpers.DTOMentee;
using MentorshipHub.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MentorshipHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenteeController : ControllerBase
    {
        private readonly IMenteeService menteeService;
        private readonly TaskSettings taskOptions;

        public MenteeController(IMenteeService menteeService , IOptions<TaskSettings> taskOptions)
        {
            this.menteeService = menteeService;
            this.taskOptions = taskOptions.Value;
        }

        [HttpPost("sendTaskSolution")]
        public async Task<IActionResult> SendTaskSolution([FromForm]DTOSendTaskSolution dTOSendTaskSolution)
        {
            var TaskFile = UploadTask(dTOSendTaskSolution.File);
            if (!TaskFile.IsSuccess)
            {
                return BadRequest(new { Error = TaskFile.Message });
            }

            var path = TaskFile.Url;
            var result = await menteeService.SendTaskSolution(dTOSendTaskSolution, path);
            return Ok(new {Message = result });
        }

        private UploadDto UploadTask(IFormFile file)
        {
            #region ValidationExtensions
            var extension = Path.GetExtension(file.FileName);
            
            //TODO: extension validation in appsettings.json file this best practice
            
            var extensions = taskOptions.AllowedExtensions;
            
            var isAllowedExtension = extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
            
            if (!isAllowedExtension)
            {
                return new UploadDto
                {
                    IsSuccess = false,
                    Message = "Invalid file extension"
                };
            }
            #endregion
            
            #region ValidationSize
            
            var MaxSize = taskOptions.MaxSize;
            var isSizeValid = file.Length >= 0 && file.Length <= MaxSize;
            if (!isSizeValid)
            {
                return new UploadDto
                {
                    IsSuccess = false,
                    Message = "Size is not allowed"
                };
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
            
            return upload;
            #endregion

        }
    }
}
