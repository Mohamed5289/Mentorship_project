using MentorshipHub.Core.DTOHelpers;
using Microsoft.AspNetCore.Identity;

namespace MentorshipHub.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public DateOnly? DateOfBirth { get; set; } = null;
        public virtual List<RefreshToken> RefreshTokens { get; set; }
        public virtual List<MentorshipRegistration> MentorshipRegistrations { get; set; } = new List<MentorshipRegistration>();
        public virtual Mentee Mentee { get; set; }
        public virtual Mentor Mentor { get; set; }
        public virtual Admin Admin { get; set; }
    }
}
