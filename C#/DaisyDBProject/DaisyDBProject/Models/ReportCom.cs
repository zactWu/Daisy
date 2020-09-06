using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReportCom
    {
        public string ReportId { get; set; }
        public string CommentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Report Report { get; set; }
    }
}
