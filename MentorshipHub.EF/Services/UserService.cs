using MentorshipHub.Core.DTOHelpers.DTOMentee;
using MentorshipHub.Core.DTOHelpers.DTOMentor;
using MentorshipHub.Core.IServices;
using MentorshipHub.Core.Models;
using MentorshipHub.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipHub.EF.Services
{
    public class UserService : IUserService
    {
        private readonly AppIdentityDbContext _context;
        public UserService(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<DTOMentorship>> Mentorships()
        {
            var mentorships = await _context.Mentorships.ToListAsync();

            return mentorships.Select(m => new DTOMentorship
            {
                Id = m.MentorshipId,
                Name = m.Title,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate
            }).ToList();
        }

        public async Task<string> RegistrationInMentorship(DTORegistrationInMentorship dTORegistrationInMentorship)
        {
            var isFound = await _context.MentorshipRegistrations.FirstOrDefaultAsync(m => m.UserId == dTORegistrationInMentorship.UserId);

            if (isFound is not null)
                return "You are registered";

            var mentorship = await _context.Mentorships.FirstOrDefaultAsync(m => m.MentorshipId == dTORegistrationInMentorship.MentorshipId);
            if (mentorship is null)
                return "Mentors is not Found";

            var mentorId = mentorship.MentorId;


            var mentorshipRegistration = new MentorshipRegistration
            {
                UserId = dTORegistrationInMentorship.UserId,
                MentorshipId = dTORegistrationInMentorship.MentorshipId,
                MentorId = mentorId,
                Status = "Pending"

            };

            await _context.MentorshipRegistrations.AddAsync(mentorshipRegistration);
            await _context.SaveChangesAsync();

            return "Registration successful";
        }
    }
}
