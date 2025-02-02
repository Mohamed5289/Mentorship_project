namespace MentorshipHub.Core.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; } = null!;
    }

}
