namespace MentorshipHub.Api.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int AverageRating { get; set; }
        public string Feedback { get; set; } = string.Empty;
        public int MentorshipId { get; set; }
        public virtual Mentorship Mentorship { get; set; } = null!;
        public int MenteeId { get; set; }
        public virtual Mentee Mentee { get; set; } = null!;
    }
}
