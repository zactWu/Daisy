using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class UserNotifi
    {
        public string ProjectId { get; set; }
        public string NotifiId { get; set; }
        public string Account { get; set; }
        public int? ReadTag { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
