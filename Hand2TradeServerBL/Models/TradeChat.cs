using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class TradeChat
    {
        public TradeChat()
        {
            TextMessages = new HashSet<TextMessage>();
        }

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
      
        [InverseProperty(nameof(User.TradeChatBuyers))]
        public virtual User Buyer { get; set; }
        [ForeignKey(nameof(ItemId))]
        [InverseProperty("TradeChats")]
        public virtual Item Item { get; set; }
        [ForeignKey(nameof(SellerId))]
        [InverseProperty(nameof(User.TradeChatSellers))]
        public virtual User Seller { get; set; }
        [InverseProperty(nameof(TextMessage.Chat))]
        public virtual ICollection<TextMessage> TextMessages { get; set; }
    }
}
