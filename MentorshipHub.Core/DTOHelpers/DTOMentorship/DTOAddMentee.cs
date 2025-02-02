using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipHub.Core.DTOHelpers.DTOMentorship
{
    public class DTOAddMentee
    {
        public string MentorshipTitle { get; set; }
        public string UserId { get; set; }
        public bool Succeed { get; set; } = false;
    }
}
