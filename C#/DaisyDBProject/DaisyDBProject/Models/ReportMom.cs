using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReportMom
    {
        public string ReportId { get; set; }
        public string MomentId { get; set; }

        public virtual Moment Moment { get; set; }
        public virtual Report Report { get; set; }
    }
}
