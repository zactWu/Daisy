using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReportReply
    {
        public string ReportId { get; set; }
        public string ReplyId { get; set; }

        public virtual Reply Reply { get; set; }
        public virtual Report Report { get; set; }
    }
}
