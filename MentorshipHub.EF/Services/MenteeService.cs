using MentorshipHub.Core.DTOHelpers.DTOMentee;
using MentorshipHub.Core.IServices;
using MentorshipHub.Core.Models;
using MentorshipHub.EF.Data;
using Microsoft.EntityFrameworkCore;

namespace MentorshipHub.EF.Services
{
    public class MenteeService : IMenteeService
    {
        private readonly AppIdentityDbContext _context;
        public MenteeService(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<string> SendTaskSolution(DTOSendTaskSolution dTOSendTaskSolution , string path)
        {
          var taskSubmission = new TaskSubmission
          {
              Solution = path,
              TaskId = dTOSendTaskSolution.TaskId,
              MenteeId = dTOSendTaskSolution.MenteeId,
              Status = "Pending",
          };
          await _context.TaskSubmissions.AddAsync(taskSubmission);
          await _context.SaveChangesAsync();
            return "Task solution sent";
        }
    }
}
