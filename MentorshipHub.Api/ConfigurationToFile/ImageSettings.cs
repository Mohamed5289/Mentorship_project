namespace MentorshipHub.Api.ConfigurationToFile
{
    public class ImageSettings
    {
        public ICollection<string> AllowedExtensions { get; set; } 

        public int MaxSize { get; set; }
    }
}
