﻿namespace MentorshipHub.Core.Models
{
    public class Mentee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AppUserId { get; set;}
        public virtual AppUser AppUser { get; set; } = null!;
        public virtual List<Mentorship> Mentorships { get; set; } = new List<Mentorship>();
        public virtual List<Achievement> Achievements { get; set; } = new List<Achievement>();
        public virtual List<TaskOfMentorship> Tasks { get; set; } = new List<TaskOfMentorship>();
        public virtual List<TaskSubmission> TaskSubmissions { get; set; } = new List<TaskSubmission>();

    }
}
