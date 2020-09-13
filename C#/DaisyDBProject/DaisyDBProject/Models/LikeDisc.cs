using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class LikeDisc
    {
        public int DiscussionId { get; set; }
        public int ProjectId { get; set; }
        public string Account { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual Discussion Discussion { get; set; }
    }
}
