using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReportMom
    {
        public int ReportId { get; set; }
        public int MomentId { get; set; }

        public virtual Moment Moment { get; set; }
        public virtual Report Report { get; set; }
    }
}
