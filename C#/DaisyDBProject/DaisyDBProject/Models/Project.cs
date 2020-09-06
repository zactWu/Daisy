using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Project
    {
        public Project()
        {
            Discussion = new HashSet<Discussion>();
            Notification = new HashSet<Notification>();
            Subscribe = new HashSet<Subscribe>();
            Usergroups = new HashSet<Usergroups>();
        }

        public string ProjectId { get; set; }
        public string AdministratorId { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public decimal? ParticipantsNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Host { get; set; }

        public virtual Administrator Administrator { get; set; }
        public virtual ICollection<Discussion> Discussion { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<Subscribe> Subscribe { get; set; }
        public virtual ICollection<Usergroups> Usergroups { get; set; }
    }
}
