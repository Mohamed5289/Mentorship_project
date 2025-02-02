using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MentorshipHub.Core.DTOHelpers.DTOMentee
{
    public class DTOSendTaskSolution
    {
        public int TaskId { get; set; }
        public int MenteeId { get; set; }
        public IFormFile File { get; set; }

    }
}
