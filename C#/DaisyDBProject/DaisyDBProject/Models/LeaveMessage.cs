using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class LeaveMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveMessageId { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public int? ReadTag { get; set; }

        public virtual Users AccountNavigation { get; set; }
    }
}
