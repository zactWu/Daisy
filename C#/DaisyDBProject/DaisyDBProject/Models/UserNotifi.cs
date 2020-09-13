using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class UserNotifi
    {
        public int ProjectId { get; set; }
        public int NotifiId { get; set; }
        public string Account { get; set; }
        public int? ReadTag { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
