using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class Rating
    {
        [Key]
        [Column("ratingID")]
        public int RatingId { get; set; }
        [Column("ratedUserID")]
        public int RatedUserId { get; set; }
        [Column("senderID")]
        public int SenderId { get; set; }
        [Column("rate")]
        public double Rate { get; set; }

        [ForeignKey(nameof(RatedUserId))]
        [InverseProperty(nameof(User.RatingRatedUsers))]
        public virtual User RatedUser { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.RatingSenders))]
        public virtual User Sender { get; set; }
    }
}
