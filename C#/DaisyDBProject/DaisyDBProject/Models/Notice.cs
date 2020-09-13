using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Notice
    {
        public Notice()
        {
            UserNotice = new HashSet<UserNotice>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoticeId { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }

        public virtual ICollection<UserNotice> UserNotice { get; set; }
    }
}
