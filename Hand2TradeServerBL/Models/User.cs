using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    [Index(nameof(Email), Name = "UC_email", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Items = new HashSet<Item>();
            LikedItems = new HashSet<LikedItem>();
            RatingRatedUsers = new HashSet<Rating>();
            RatingSenders = new HashSet<Rating>();
            ReportReportedUsers = new HashSet<Report>();
            ReportSenders = new HashSet<Report>();
            TextMessages = new HashSet<TextMessage>();
            TradeChatBuyers = new HashSet<TradeChat>();
            TradeChatSellers = new HashSet<TradeChat>();
        }

        [Key]
        [Column("userID")]
        public int UserId { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("passwrd")]
        [StringLength(30)]
        public string Passwrd { get; set; }
        [Required]
        [Column("userName")]
        [StringLength(30)]
        public string UserName { get; set; }
        [Column("isAdmin")]
        public bool IsAdmin { get; set; }
        [Column("coins")]
        public int Coins { get; set; }
        [Column("reports")]
        public int Reports { get; set; }
        [Column("sumRanks")]
        public double SumRanks { get; set; }
        [Column("countRanked")]
        public int CountRanked { get; set; }
        [Column("bearthDate", TypeName = "datetime")]
        public DateTime BearthDate { get; set; }
        [Required]
        [Column("adress")]
        [StringLength(255)]
        public string Adress { get; set; }
        [Column("isBlocked")]
        public bool IsBlocked { get; set; }
        [Column("joinedDate", TypeName = "datetime")]       
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty(nameof(LikedItem.Sender))]
        public virtual ICollection<LikedItem> LikedItems { get; set; }
        [InverseProperty(nameof(Rating.RatedUser))]
        public virtual ICollection<Rating> RatingRatedUsers { get; set; }
        [InverseProperty(nameof(Rating.Sender))]
        public virtual ICollection<Rating> RatingSenders { get; set; }
        [InverseProperty(nameof(Report.ReportedUser))]
        public virtual ICollection<Report> ReportReportedUsers { get; set; }
        [InverseProperty(nameof(Report.Sender))]
        public virtual ICollection<Report> ReportSenders { get; set; }
        [InverseProperty(nameof(TextMessage.Sender))]
        public virtual ICollection<TextMessage> TextMessages { get; set; }
        [InverseProperty(nameof(TradeChat.Buyer))]
        public virtual ICollection<TradeChat> TradeChatBuyers { get; set; }
        [InverseProperty(nameof(TradeChat.Seller))]
        public virtual ICollection<TradeChat> TradeChatSellers { get; set; }
    }
}
