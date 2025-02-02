namespace MentorshipHub.Core.Models
{
    public class TaskSubmission
    {
        public int TaskSubmissionId { get; set; }
        public string Solution { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Grade { get; set; }
        public DateOnly DueDate { get; set; }
        public string Feedback { get; set; } = string.Empty;
        public int TaskId { get; set; }
        public virtual TaskOfMentorship Task { get; set; } = null!;
        public int MenteeId { get; set; }
        public virtual Mentee Mentee { get; set; } = null!;



    }
}
