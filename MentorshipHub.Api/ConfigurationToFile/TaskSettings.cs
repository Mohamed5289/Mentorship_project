namespace MentorshipHub.Api.ConfigurationToFile
{
    public class TaskSettings
    {
        public ICollection<string> AllowedExtensions { get; set; }
        public int MaxSize { get; set; }
    }
}
