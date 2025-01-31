
using System.ComponentModel.DataAnnotations;

namespace MentorshipHub.Api.DTOHelpers
{
    public class RegisterModel
    {
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
