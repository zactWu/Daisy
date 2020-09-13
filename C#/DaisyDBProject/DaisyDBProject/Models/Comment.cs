using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Reply = new HashSet<Reply>();
            ReportCom = new HashSet<ReportCom>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int MomentId { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Moment Moment { get; set; }
        public virtual ICollection<Reply> Reply { get; set; }
        public virtual ICollection<ReportCom> ReportCom { get; set; }
    }
}
