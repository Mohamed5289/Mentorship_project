namespace MentorshipHub.Core.Models
{
    public class TaskOfMentorship
    {
        public int TaskId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public DateOnly CreationTime { get; set; }
        public int MentorshipId { get; set; }
        public  virtual Mentorship Mentorship { get; set; } 
        public virtual List<TaskSubmission> TaskSubmissions { get; set; } = new List<TaskSubmission>();

    }
}
