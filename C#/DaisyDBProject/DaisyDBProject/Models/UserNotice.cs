using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class UserNotice
    {
        public string NoticeId { get; set; }
        public string Account { get; set; }
        public decimal? ReadTag { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Notice Notice { get; set; }
    }
}
