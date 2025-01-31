namespace MentorshipHub.Api.ConfigurationToFile
{
    public class UploadDto
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
