using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class LeaveMessage
    {
        public string LeaveMessageId { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public decimal? ReadTag { get; set; }

        public virtual Users AccountNavigation { get; set; }
    }
}
