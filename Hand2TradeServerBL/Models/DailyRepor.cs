using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class DailyRepor
    {
        [Key]
        [Column("dailyReportReportID")]
        public int DailyReportReportId { get; set; }
        [Column("dateOfDay", TypeName = "datetime")]
        public DateTime DateOfDay { get; set; }
        [Column("newSubs")]
        public int NewSubs { get; set; }
        [Column("itemsTraded")]
        public int ItemsTraded { get; set; }
        [Column("loansTaken")]
        public int LoansTaken { get; set; }
        [Column("loansDeptPaid")]
        public int LoansDeptPaid { get; set; }
        [Column("reportsNum")]
        public int ReportsNum { get; set; }
    }
}
