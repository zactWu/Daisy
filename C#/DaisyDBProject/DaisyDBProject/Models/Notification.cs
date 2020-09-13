using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Notification
    {
        public Notification()
        {
            UserNotifi = new HashSet<UserNotifi>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotifiId { get; set; }
        public int ProjectId { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<UserNotifi> UserNotifi { get; set; }
    }
}
