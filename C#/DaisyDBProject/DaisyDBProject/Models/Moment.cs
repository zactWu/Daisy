using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Moment
    {
        public Moment()
        {
            Comment = new HashSet<Comment>();
            LikeMoment = new HashSet<LikeMoment>();
            MomentStar = new HashSet<MomentStar>();
            ReportMom = new HashSet<ReportMom>();
        }

        public string MomentId { get; set; }
        public string Account { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string ContentUrl { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<LikeMoment> LikeMoment { get; set; }
        public virtual ICollection<MomentStar> MomentStar { get; set; }
        public virtual ICollection<ReportMom> ReportMom { get; set; }
    }
}
