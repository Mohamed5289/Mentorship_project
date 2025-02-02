namespace MentorshipHub.Core.DTOHelpers.DTOMentor
{
    public class DTOMentorship
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
