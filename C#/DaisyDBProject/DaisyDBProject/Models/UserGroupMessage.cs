using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class UserGroupMessage
    {
        public string GroupMessageId { get; set; }
        public string Account { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual GroupMessage GroupMessage { get; set; }
    }
}
