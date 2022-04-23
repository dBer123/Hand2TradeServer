using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class LikedItem
    {
        [Key]
        [Column("likedItemID")]
        public int LikedItemId { get; set; }
        [Column("itemID")]
        public int ItemId { get; set; }
        [Column("senderID")]
        public int SenderId { get; set; }

        [ForeignKey(nameof(ItemId))]
        [InverseProperty("LikedItems")]
        public virtual Item Item { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.LikedItems))]
        public virtual User Sender { get; set; }
    }
}
