using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Usergroups
    {
        public Usergroups()
        {
            Application = new HashSet<Application>();
            Member = new HashSet<Member>();
            Post = new HashSet<Post>();
        }

        public string GroupId { get; set; }
        public string ProjectId { get; set; }
        public string LeaderAccount { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }

        public virtual Users LeaderAccountNavigation { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Application> Application { get; set; }
        public virtual ICollection<Member> Member { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
