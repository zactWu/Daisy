﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class LiftBan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LiftBanId { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public string DealStatus { get; set; }

        public virtual Users AccountNavigation { get; set; }
    }
}
