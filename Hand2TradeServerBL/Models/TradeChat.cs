﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    [Table("TradeChat")]
    public partial class TradeChat
    {
        [Key]
        [Column("chatID")]
        public int ChatId { get; set; }
        [Column("itemID")]
        public int ItemId { get; set; }
        [Column("buyerID")]
        public int BuyerId { get; set; }
        [Column("sellerID")]
        public int SellerId { get; set; }
        [Column("isTradeAgreed")]
        public bool IsTradeAgreed { get; set; }

        [ForeignKey(nameof(BuyerId))]
        [InverseProperty(nameof(User.TradeChatBuyers))]
        public virtual User Buyer { get; set; }
        [ForeignKey(nameof(ItemId))]
        [InverseProperty("TradeChats")]
        public virtual Item Item { get; set; }
        [ForeignKey(nameof(SellerId))]
        [InverseProperty(nameof(User.TradeChatSellers))]
        public virtual User Seller { get; set; }
    }
}