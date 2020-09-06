using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Follow
    {
        public string FollowedAccount { get; set; }
        public string Account { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Users FollowedAccountNavigation { get; set; }
    }
}
