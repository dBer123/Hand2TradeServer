﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    [Table("Loan")]
    public partial class Loan
    {
        [Key]
        [Column("LoanID")]
        public int LoanId { get; set; }
        [Column("loanerID")]
        public int LoanerId { get; set; }
        [Column("coinsLoaned")]
        public int CoinsLoaned { get; set; }
        [Column("debt")]
        public int Debt { get; set; }
        [Column("isDebtPaid")]
        public bool IsDebtPaid { get; set; }
        [Column("paymentDate", TypeName = "datetime")]
        public DateTime PaymentDate { get; set; }

        [ForeignKey(nameof(LoanerId))]
        [InverseProperty(nameof(User.Loans))]
        public virtual User Loaner { get; set; }
    }
}