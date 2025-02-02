namespace MentorshipHub.Core.Models
{
    public class Mentorship
    {
        public int MentorshipId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string status { get; set; }
        public int Hours { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int MentorId { get; set; }
        public virtual Mentor Mentor { get; set; } = null!;
        public virtual List<Mentee> Mentees { get; set; }
        public virtual List<Achievement> Achievements { get; set; } 
        public virtual List<TaskOfMentorship> Tasks { get; set; } 
        public virtual List<MentorshipRegistration> MentorshipRegistrations { get; set; } 

    }
}
