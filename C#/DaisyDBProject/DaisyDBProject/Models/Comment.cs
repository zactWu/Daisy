using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Reply = new HashSet<Reply>();
            ReportCom = new HashSet<ReportCom>();
        }

        public string CommentId { get; set; }
        public string MomentId { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Moment Moment { get; set; }
        public virtual ICollection<Reply> Reply { get; set; }
        public virtual ICollection<ReportCom> ReportCom { get; set; }
    }
}
