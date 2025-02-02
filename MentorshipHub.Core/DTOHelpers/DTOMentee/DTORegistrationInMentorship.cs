using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipHub.Core.DTOHelpers.DTOMentee
{
    public class DTORegistrationInMentorship
    {
        public int MentorshipId  { get; set; }
        public string UserId { get; set; }
    }
}
