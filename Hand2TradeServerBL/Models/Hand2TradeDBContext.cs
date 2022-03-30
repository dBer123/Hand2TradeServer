using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Hand2TradeServerBL.Models
{
    public partial class Hand2TradeDBContext : DbContext
    {
        public Hand2TradeDBContext()
        {
        }

        public Hand2TradeDBContext(DbContextOptions<Hand2TradeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DailyRepor> DailyRepors { get; set; }
        public virtual DbSet<HourlyReport> HourlyReports { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<MonthlyReport> MonthlyReports { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<TextMessage> TextMessages { get; set; }
        public virtual DbSet<TradeChat> TradeChats { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS; Database=Hand2TradeDB; Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<DailyRepor>(entity =>
            {
                entity.HasKey(e => e.DailyReportReportId)
                    .HasName("PK__DailyRep__1CAD8966B5FA78CE");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Items__userID__398D8EEE");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasOne(d => d.Loaner)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__loanerID__44FF419A");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasOne(d => d.RatedUser)
                    .WithMany(p => p.RatingRatedUsers)
                    .HasForeignKey(d => d.RatedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__ratedUs__5165187F");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.RatingSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__senderI__52593CB8");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasOne(d => d.ReportedUser)
                    .WithMany(p => p.ReportReportedUsers)
                    .HasForeignKey(d => d.ReportedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reports__reporte__4D94879B");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ReportSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reports__senderI__4E88ABD4");
            });

            modelBuilder.Entity<TextMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__TextMess__4808B8735201CCE1");

                entity.Property(e => e.TextMessage1).IsUnicode(false);

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.TextMessages)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TextMessa__chatI__412EB0B6");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.TextMessages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TextMessa__sende__4222D4EF");
            });

            modelBuilder.Entity<TradeChat>(entity =>
            {
                entity.HasKey(e => e.ChatId)
                    .HasName("PK__TradeCha__8263854DC10FF3DE");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.TradeChatBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeChat__buyer__3D5E1FD2");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TradeChats)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeChat__itemI__3C69FB99");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.TradeChatSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeChat__selle__3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
