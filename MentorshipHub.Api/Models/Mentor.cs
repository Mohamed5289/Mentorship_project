namespace MentorshipHub.Api.Models
{
    public class Mentor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AppUserId { get; set; }
        public  virtual AppUser AppUser { get; set; } = null!;
        public virtual List<Mentorship> Mentorships { get; set; } = new List<Mentorship>();
        public virtual List<MentorshipRegistration> MentorshipRegistrations { get; set; } = new List<MentorshipRegistration>();
    }
}
