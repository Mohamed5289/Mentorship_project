using MentorshipHub.Core.DTOHelpers.DTOMentee;
using MentorshipHub.Core.DTOHelpers.DTOMentor;
using MentorshipHub.Core.DTOHelpers.DTOMentorship;
using MentorshipHub.Core.Models;

namespace MentorshipHub.Core.IServices
{
    public interface IMentorService
    {
        Task<List<DTOMentorship>> GetMentorshipsByMentorId(int mentorId);
        Task<string> AddTask(DTOAddTask dTOAddTask);
        Task<List<DTOMentee>> MenteesOfMentorship (string mentorshipTitle);
        Task<string> AddMentee(DTOAddMentee dTOAddMentee);
    }
}
