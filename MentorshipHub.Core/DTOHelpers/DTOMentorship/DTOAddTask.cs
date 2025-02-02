using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipHub.Core.DTOHelpers.DTOMentorship
{
    public class DTOAddTask
    {
       public string MentorshipTitle { get; set; }
       public string Description { get; set; }
       public DateOnly Deadline { get; set; }
    }
}
