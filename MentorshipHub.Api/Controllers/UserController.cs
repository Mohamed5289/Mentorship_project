using MentorshipHub.Core.DTOHelpers.DTOMentee;
using MentorshipHub.Core.DTOHelpers.DTOMentor;
using MentorshipHub.Core.IServices;
using MentorshipHub.EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace MentorshipHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("registrationInMentorship")]
        public async Task<IActionResult> RegistrationInMentorship(DTORegistrationInMentorship dTORegistrationInMentorship)
        {
            var result = await userService.RegistrationInMentorship(dTORegistrationInMentorship);
            if(result == "Registration successful")
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("mentorships")]
        public async Task<IActionResult> Mentorships()
        {
            var result = await userService.Mentorships();

            return Ok(result);
        }
    }
}
