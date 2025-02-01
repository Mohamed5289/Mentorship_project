namespace MentorshipHub.Api.Models
{
    public class Mentorship
    {
        public int MentorshipId { get; set; }
        public string Discription { get; set; }
        public string Title { get; set; }
        public string status { get; set; }
        public int Hours { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int MentorId { get; set; }
        public virtual Mentor Mentor { get; set; } = null!;
        public virtual List<Mentee> Mentees { get; set; } = new List<Mentee>();
        public virtual List<Achievement> Achievements { get; set; } = new List<Achievement>();
        public virtual List<Task> Tasks { get; set; } = new List<Task>();
        public virtual List<MentorshipRegistration> MentorshipRegistrations { get; set; } = new List<MentorshipRegistration>();

    }
}
