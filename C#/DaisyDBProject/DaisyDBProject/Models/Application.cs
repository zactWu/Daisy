using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Application
    {
        public string ProjectId { get; set; }
        public string GroupId { get; set; }
        public string Account { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Usergroups Usergroups { get; set; }
    }
}
