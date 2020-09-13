using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Usergroups
    {
        public Usergroups()
        {
            Application = new HashSet<Application>();
            GroupMessage = new HashSet<GroupMessage>();
            Member = new HashSet<Member>();
            Post = new HashSet<Post>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }
        public int ProjectId { get; set; }
        public string LeaderAccount { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }

        public virtual Users LeaderAccountNavigation { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Application> Application { get; set; }
        public virtual ICollection<GroupMessage> GroupMessage { get; set; }
        public virtual ICollection<Member> Member { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
