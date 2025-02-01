namespace MentorshipHub.Api.Models
{
    public class MentorshipRegistration
    {
        public int MentorshipRegistrationId { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public string Status { get; set; }
        public int MentorshipId { get; set; }
        public virtual Mentorship Mentorship { get; set; } = null!;
        public int MenteeId { get; set; }
        public virtual Mentee Mentee { get; set; } = null!;
        public int MentorId { get; set; }
        public virtual Mentor Mentor { get; set; } = null!;

    }
}
