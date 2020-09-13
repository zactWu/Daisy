using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Subscribe
    {
        public int ProjectId { get; set; }
        public string Account { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Project Project { get; set; }
    }
}
