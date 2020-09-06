using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class ReplyReply
    {
        public string IdToReply { get; set; }
        public string IdReplied { get; set; }

        public virtual Reply IdRepliedNavigation { get; set; }
        public virtual Reply IdToReplyNavigation { get; set; }
    }
}
