using MentorshipHub.Core.DTOHelpers.DTOMentee;
using MentorshipHub.Core.DTOHelpers.DTOMentor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipHub.Core.IServices
{
    public interface IUserService
    {
        Task<string> RegistrationInMentorship(DTORegistrationInMentorship dTORegistrationInMentorship);
        Task<List<DTOMentorship>> Mentorships();
    }
}
