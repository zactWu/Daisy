using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Notification
    {
        public Notification()
        {
            UserNotifi = new HashSet<UserNotifi>();
        }

        public string NotifiId { get; set; }
        public string ProjectId { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public string AdministratorId { get; set; }
        public string Title { get; set; }

        public virtual Administrator Administrator { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<UserNotifi> UserNotifi { get; set; }
    }
}
