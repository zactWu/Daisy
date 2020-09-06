using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Discussion
    {
        public Discussion()
        {
            LikeDisc = new HashSet<LikeDisc>();
            ReportDisc = new HashSet<ReportDisc>();
        }

        public string DiscussionId { get; set; }
        public string ProjectId { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public string PictureUrl { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<LikeDisc> LikeDisc { get; set; }
        public virtual ICollection<ReportDisc> ReportDisc { get; set; }
    }
}
