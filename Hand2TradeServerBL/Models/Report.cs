using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class Report
    {
        [Key]
        [Column("reportID")]
        public int ReportId { get; set; }
        [Column("reportedUserID")]
        public int ReportedUserId { get; set; }
        [Column("senderID")]
        public int SenderId { get; set; }

        [ForeignKey(nameof(ReportedUserId))]
        [InverseProperty(nameof(User.ReportReportedUsers))]
        public virtual User ReportedUser { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.ReportSenders))]
        public virtual User Sender { get; set; }
    }
}
