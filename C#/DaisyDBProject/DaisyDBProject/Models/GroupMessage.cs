using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class GroupMessage
    {
        public GroupMessage()
        {
            UserGroupMessage = new HashSet<UserGroupMessage>();
        }

        public string GroupMessageId { get; set; }
        public string GroupId { get; set; }
        public string ProjectId { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }

        public virtual Usergroups Usergroups { get; set; }
        public virtual ICollection<UserGroupMessage> UserGroupMessage { get; set; }
    }
}
