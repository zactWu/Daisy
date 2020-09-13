using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        public string SendAccount { get; set; }
        public string ReceiveAccount { get; set; }
        public int? ReadTag { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }

        public virtual Users ReceiveAccountNavigation { get; set; }
        public virtual Users SendAccountNavigation { get; set; }
    }
}
