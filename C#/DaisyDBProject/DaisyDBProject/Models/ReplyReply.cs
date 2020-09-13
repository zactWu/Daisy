using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReplyReply
    {
        public int IdToReply { get; set; }
        public int IdReplied { get; set; }

        public virtual Reply IdRepliedNavigation { get; set; }
        public virtual Reply IdToReplyNavigation { get; set; }
    }
}
