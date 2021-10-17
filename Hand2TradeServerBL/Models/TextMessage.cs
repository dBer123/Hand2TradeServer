using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    [Table("TextMessage")]
    public partial class TextMessage
    {
        [Key]
        [Column("messageID")]
        public int MessageId { get; set; }
        [Column("chatID")]
        public int ChatId { get; set; }
        [Column("senderID")]
        public int SenderId { get; set; }
        [Required]
        [Column("textMessage")]
        [StringLength(255)]
        public string TextMessage1 { get; set; }

        [ForeignKey(nameof(ChatId))]
        [InverseProperty(nameof(Item.TextMessages))]
        public virtual Item Chat { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.TextMessages))]
        public virtual User Sender { get; set; }
    }
}
