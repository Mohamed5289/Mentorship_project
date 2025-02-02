using MentorshipHub.Core.DTOHelpers.DTOMentee;

namespace MentorshipHub.Core.IServices
{
    public interface IMenteeService
    {
        Task<string> SendTaskSolution(DTOSendTaskSolution dTOSendTaskSolution , string path);
    }
}
