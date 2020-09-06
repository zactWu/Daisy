using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class LikeMoment
    {
        public string MomentId { get; set; }
        public string Account { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Moment Moment { get; set; }
    }
}
