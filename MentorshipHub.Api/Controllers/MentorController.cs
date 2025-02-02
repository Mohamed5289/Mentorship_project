using MentorshipHub.Core.DTOHelpers.DTOMentorship;
using MentorshipHub.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MentorshipHub.Api.Controllers
{
    [Route("api/mentor")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public MentorController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }

        [HttpGet("mentorshipsById")]
        public async Task<IActionResult> GetMentorships(int mentorId)
        {
            if (mentorId <= 0)
            {
                return BadRequest(new { Message = "Invalid mentor ID" });
            }

            var mentorships = await _mentorService.GetMentorshipsByMentorId(mentorId);
            return Ok(mentorships);
        }


        [HttpPost("add-mentee")]
        public async Task<IActionResult> AddMentee(DTOAddMentee dTOAddMentee)
        {
            var result = await _mentorService.AddMentee(dTOAddMentee);

            if (result == "Mentee added successfully")
                return Ok(new { Message = result });

            return BadRequest(new { Error = result });
        }

        [HttpPost("add-task")]
        public async Task<IActionResult> AddTask(DTOAddTask dTOAddTask)
        {
            var result = await _mentorService.AddTask(dTOAddTask);

            if (result == "Task is added successfully")
                return Ok(new { Message = result });

            return BadRequest(new { Error = result });
        }

        [HttpGet("mentees")]
        public async Task<IActionResult> MenteesOfMentorship(string mentorshipTitle)
        {
            if (string.IsNullOrWhiteSpace(mentorshipTitle))
            {
                return BadRequest(new { Message = "Mentorship title cannot be empty" });
            }

            var mentees = await _mentorService.MenteesOfMentorship(mentorshipTitle);
            return Ok(mentees);
        }

        //[HttpGet("mentees-registration")]
        //public async Task<IActionResult> MenteesOfMentorshipRegistration(string mentorshipTitle)
        //{
        //    if (string.IsNullOrWhiteSpace(mentorshipTitle))
        //    {
        //        return BadRequest(new { Message = "Mentorship title cannot be empty" });
        //    }

        //    var mentees = await _mentorService.MenteesOfMentorshipRegistration(mentorshipTitle);
        //    return Ok(mentees);
        //}
    }
}
