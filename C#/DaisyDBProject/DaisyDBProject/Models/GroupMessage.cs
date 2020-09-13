using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class GroupMessage
    {
        public GroupMessage()
        {
            UserGroupMessage = new HashSet<UserGroupMessage>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupMessageId { get; set; }
        public int GroupId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }

        public virtual Usergroups Usergroups { get; set; }
        public virtual ICollection<UserGroupMessage> UserGroupMessage { get; set; }
    }
}
