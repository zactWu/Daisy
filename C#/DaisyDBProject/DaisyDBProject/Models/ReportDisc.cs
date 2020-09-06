using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReportDisc
    {
        public string ReportId { get; set; }
        public string ProjectId { get; set; }
        public string DiscussionId { get; set; }

        public virtual Discussion Discussion { get; set; }
        public virtual Report Report { get; set; }
    }
}
