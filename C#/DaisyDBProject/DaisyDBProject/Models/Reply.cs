using System;
using System.Collections.Generic;

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

        public string ReplyId { get; set; }
        public string CommentId { get; set; }
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
