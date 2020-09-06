using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Notice
    {
        public Notice()
        {
            UserNotice = new HashSet<UserNotice>();
        }

        public string NoticeId { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }

        public virtual ICollection<UserNotice> UserNotice { get; set; }
    }
}
