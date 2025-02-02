using MentorshipHub.EF.Data;
using MentorshipHub.Core.DTOHelpers.DTOMentee;
using MentorshipHub.Core.DTOHelpers.DTOMentor;
using MentorshipHub.Core.IServices;
using Microsoft.EntityFrameworkCore;
using MentorshipHub.Core.Models;
using MentorshipHub.Core.DTOHelpers.DTOMentorship;
using Microsoft.OpenApi.Validations;
using Microsoft.AspNetCore.Identity;

namespace MentorshipHub.EF.Services
{
    public class MentorService : IMentorService
    {
        private readonly AppIdentityDbContext context;
        private readonly UserManager<AppUser> userManager;
        public MentorService(AppIdentityDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<string> AddMentee(DTOAddMentee dTOAddMentee)
        {
            var user =  await context.Users.FirstOrDefaultAsync(m => m.Id == dTOAddMentee.UserId);

            if (user is null)
                return "Authentication";

            var mentorshipRegistration= await context.MentorshipRegistrations.FirstOrDefaultAsync(m => m.UserId == user.Id);
            mentorshipRegistration!.Status = "Accept";
            await context.SaveChangesAsync();

            if (!context.Mentees.Any(m => m.AppUserId == dTOAddMentee.UserId))
            {
                var mentee = new Mentee
                {
                    AppUserId = dTOAddMentee.UserId,
                    Name = user!.UserName
                };
                await context.Mentees.AddAsync(mentee);
                await userManager.AddToRoleAsync(user, "Mentee");
            }

            var addMentee  = await context.Mentees.FirstOrDefaultAsync(m => m.AppUserId == dTOAddMentee.UserId);
            var mentorship  = await context.Mentorships.FirstOrDefaultAsync(m => m.Title == dTOAddMentee.MentorshipTitle);

            if (mentorship is null)
                return "Mentorship is not found";
            await context.Achievements.AddAsync(new Achievement
            {
                MenteeId = addMentee!.Id,
                MentorshipId = mentorship.MentorshipId
            });
            return "Successful";

        }

        public async Task<string> AddTask(DTOAddTask dTOAddTask)
        {
            var mentorship = await context.Mentorships.FirstOrDefaultAsync(m => m.Title == dTOAddTask.MentorshipTitle);
            if (mentorship is null)
                return "Mentorship is not found";

            var task = await context.TaskOfMentorships.AddAsync(new TaskOfMentorship
            {
                Description = dTOAddTask.Description,
                Deadline = dTOAddTask.Deadline,
                MentorshipId = mentorship.MentorshipId
            });
            if (task is null)
                return "Task is not added";
            return "Task is added successfully";
        }

        public async Task<List<DTOMentorship>> GetMentorshipsByMentorId(int mentorId)
        {
            var mentorships = await context.Mentorships
                .Where(m => m.MentorId == mentorId)
                .Select(m =>new DTOMentorship
                {
                    Id = m.MentorshipId,
                    Name = m.Title,
                    Description = m.Description
                }).ToListAsync();
            return mentorships;

        }

        public async Task<List<DTOMentee>> MenteesOfMentorship(string mentorshipTitle)
        {
            var mentorship = await context.Mentorships.FirstOrDefaultAsync(m => m.Title == mentorshipTitle);
                
            if (mentorship is null)
                return new List<DTOMentee>();
            
            var mentees = mentorship.Mentees.Select(m => new DTOMentee
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.AppUser.Email??"",
                Username = m.AppUser.UserName?? ""
            }).ToList();

            return mentees;

        }
    }
}
