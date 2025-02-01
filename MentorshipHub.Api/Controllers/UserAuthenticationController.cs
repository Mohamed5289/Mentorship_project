using MentorshipHub.Api.ConfigurationToFile;
using MentorshipHub.Api.DTOHelpers;
using MentorshipHub.Api.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MentorshipHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {

        private readonly IUserAuthenticationService _userAuthenticationService;
        private ImageSettings _imageOptions;
        public UserAuthenticationController(IUserAuthenticationService userAuthenticationService , IOptions<ImageSettings> imageOptions)
        {
            _userAuthenticationService = userAuthenticationService;
            _imageOptions = imageOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            var result = await _userAuthenticationService.Login(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            string? imageUrl = "";

            if (model.ProfilePicture != null)
            {
                var uploadResult = UploadImage(model.ProfilePicture);
                if (!uploadResult.IsSuccess)
                    return BadRequest(new {Error = uploadResult.Message });
                imageUrl = uploadResult.Url;
            }

            var result = await _userAuthenticationService.Registering(model , imageUrl);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);


            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                IsEssential = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        private UploadDto UploadImage(IFormFile file)
        {
            #region ValidationExtensions
            var extension = Path.GetExtension(file.FileName);

            //TODO: extension validation in appsettings.json file this best practice

            var extensions = _imageOptions.AllowedExtensions;

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

            var MaxSize = _imageOptions.MaxSize;
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

            return upload;
            #endregion

        }

    }
}
