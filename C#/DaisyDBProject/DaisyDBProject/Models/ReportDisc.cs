using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReportDisc
    {
        public int ReportId { get; set; }
        public int ProjectId { get; set; }
        public int DiscussionId { get; set; }

        public virtual Discussion Discussion { get; set; }
        public virtual Report Report { get; set; }
    }
}
