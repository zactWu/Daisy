using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Report
    {
        public Report()
        {
            ReportCom = new HashSet<ReportCom>();
            ReportDisc = new HashSet<ReportDisc>();
            ReportMom = new HashSet<ReportMom>();
            ReportReply = new HashSet<ReportReply>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }
        public string Account { get; set; }
        public string ReportType { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public string DealStatus { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual ICollection<ReportCom> ReportCom { get; set; }
        public virtual ICollection<ReportDisc> ReportDisc { get; set; }
        public virtual ICollection<ReportMom> ReportMom { get; set; }
        public virtual ICollection<ReportReply> ReportReply { get; set; }
    }
}
