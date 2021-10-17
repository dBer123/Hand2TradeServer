using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    [Table("MonthlyReport")]
    public partial class MonthlyReport
    {
        [Key]
        [Column("MonthlyReportID")]
        public int MonthlyReportId { get; set; }
        [Column("dateOfMonth", TypeName = "datetime")]
        public DateTime DateOfMonth { get; set; }
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
