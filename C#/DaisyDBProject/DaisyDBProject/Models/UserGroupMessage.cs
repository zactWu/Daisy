using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class UserGroupMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupMessageId { get; set; }
        public string Account { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual GroupMessage GroupMessage { get; set; }
    }
}
