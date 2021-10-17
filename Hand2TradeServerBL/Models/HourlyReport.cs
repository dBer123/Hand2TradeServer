using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    [Table("HourlyReport")]
    public partial class HourlyReport
    {
        [Key]
        [Column("HourlyReportID")]
        public int HourlyReportId { get; set; }
        [Column("hourTime", TypeName = "datetime")]
        public DateTime HourTime { get; set; }
        [Column("newSubs")]
        public int NewSubs { get; set; }
        [Column("itemsDraded")]
        public int ItemsDraded { get; set; }
        [Column("loansTaken")]
        public int LoansTaken { get; set; }
        [Column("loansDeptPaid")]
        public int LoansDeptPaid { get; set; }
        [Column("reportsNum")]
        public int ReportsNum { get; set; }
    }
}
