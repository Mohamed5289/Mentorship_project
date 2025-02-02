
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MentorshipHub.Core.DTOHelpers
{
    public class RegisterModel
    {
        public IFormFile? ProfilePicture { get; set; }
        [Required, StringLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(30)]
        public string LastName { get; set; } = string.Empty;
        [Required, StringLength(60)]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(30)]
        public string Username { get; set; } = string.Empty;
        [Required, StringLength(60)]
        public string Password { get; set; } = string.Empty;
    }
}
