using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class Item
    {
        public Item()
        {
            LikedItems = new HashSet<LikedItem>();
            TradeChats = new HashSet<TradeChat>();
        }

        [Key]
        [Column("itemID")]
        public int ItemId { get; set; }
        [Column("userID")]
        public int UserId { get; set; }
        [Required]
        [Column("itemName")]
        [StringLength(30)]
        public string ItemName { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Required]
        [Column("desrciption")]
        [StringLength(255)]
        public string Desrciption { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Items")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(LikedItem.Item))]
        public virtual ICollection<LikedItem> LikedItems { get; set; }
        [InverseProperty(nameof(TradeChat.Item))]
        public virtual ICollection<TradeChat> TradeChats { get; set; }
    }
}
