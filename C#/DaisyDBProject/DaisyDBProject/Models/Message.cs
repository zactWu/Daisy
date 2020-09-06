using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Message
    {
        public string MessageId { get; set; }
        public string SendAccount { get; set; }
        public string ReceiveAccount { get; set; }
        public decimal? ReadTag { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }

        public virtual Users ReceiveAccountNavigation { get; set; }
        public virtual Users SendAccountNavigation { get; set; }
    }
}
