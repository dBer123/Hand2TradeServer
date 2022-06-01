using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class DailyReport
    {
        [Key]
        [Column("DailyReportID")]
        public int DailyReportId { get; set; }
        [Column("dayTime", TypeName = "datetime")]
        public DateTime DayTime { get; set; }
        [Column("newSubs")]
        public int NewSubs { get; set; }
        [Column("itemsDraded")]
        public int ItemsDraded { get; set; }
        [Column("reportsNum")]
        public int ReportsNum { get; set; }
    }
}
