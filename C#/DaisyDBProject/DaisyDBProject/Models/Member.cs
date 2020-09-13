using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Member
    {
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string Account { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Usergroups Usergroups { get; set; }
    }
}
