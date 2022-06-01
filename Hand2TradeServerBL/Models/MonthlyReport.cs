using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class MonthlyReport
    {
        [Key]
        [Column("monthlyReportID")]
        public int MonthlyReportId { get; set; }
        [Column("dateOfMonth", TypeName = "datetime")]
        public DateTime DateOfMonth { get; set; }
        [Column("newSubs")]
        public int NewSubs { get; set; }
        [Column("itemsTraded")]
        public int ItemsTraded { get; set; }
        [Column("reportsNum")]
        public int ReportsNum { get; set; }
    }
}
