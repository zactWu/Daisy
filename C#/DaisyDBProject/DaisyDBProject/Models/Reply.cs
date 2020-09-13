using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Reply
    {
        public Reply()
        {
            ReplyReplyIdRepliedNavigation = new HashSet<ReplyReply>();
            ReplyReplyIdToReplyNavigation = new HashSet<ReplyReply>();
            ReportReply = new HashSet<ReportReply>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReplyId { get; set; }
        public int? CommentId { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual ICollection<ReplyReply> ReplyReplyIdRepliedNavigation { get; set; }
        public virtual ICollection<ReplyReply> ReplyReplyIdToReplyNavigation { get; set; }
        public virtual ICollection<ReportReply> ReportReply { get; set; }
    }
}
