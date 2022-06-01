using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hand2TradeServerBL.Models
{
    public partial class Hand2TradeDBContext : DbContext
    {
        public User Login(string email, string pswd)
        {
            User user = this.Users
                .Where(u => u.Email == email && u.Passwrd == pswd)
                .Include(u => u.Items).FirstOrDefault();
            if (user != null)
            {
                int countReports = user.ReportReportedUsers.Count;
                if (countReports > 40)
                {
                    user.IsBlocked = true;
                }
            }
          
            
           
            return user;
        }
        
        public User AddUser(string password, string username, string email, int coins, string adress, DateTime dDay, bool isAdmin, bool isBlocked, DateTime joinedDate)
        {
            User active = new User();
            active.Passwrd = password;
            active.Coins = coins;
            active.Email = email;
            active.UserName = username;
            active.Adress = adress;
            active.BearthDate = dDay;
            active.IsAdmin = isAdmin;
            active.IsBlocked = isBlocked;  
            active.JoinedDate = joinedDate;

            try
            {
                this.Users.Add(active);
                DailyReport dailyReport = this.DailyReports
            .Where(r => r.DayTime.Date == DateTime.Today)
            .FirstOrDefault();
                MonthlyReport monthlyReport = this.MonthlyReports
               .Where(r => r.DateOfMonth.Date.AddMonths(13) > DateTime.Today)
               .FirstOrDefault();
                dailyReport.NewSubs++;
                monthlyReport.NewSubs++;
                this.SaveChanges();
            }
            catch 
            {
                return null;
            }
            return active;

        }

       public Item AddItem(int price, string description, string name, User user)
        {
            try
            {
                Item item = new Item
                {
                    Desrciption = description,
                    Price = price,
                    
                    UserId = user.UserId,
                    ItemName = name                    
                };
                this.Items.Add(item);
                User user1 = this.Users
                   .Where(u => u.UserId == user.UserId).FirstOrDefault();
                user1.Items.Add(item);
                this.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
        }
       public void ValidatedEmail(string email)
        {
            User user = this.Users
               .Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                user.IsBlocked = false;

                this.SaveChanges();
            }
        }

        public User UpdateUser(string address, string pass, string userName, int id)
        {
            User user = this.Users
                  .Where(u => u.UserId == id).Include(u => u.Items).FirstOrDefault();
            user.Passwrd = pass;
            user.Adress = address;
            user.UserName = userName;
    
            this.SaveChanges();
            return user;
        }
        public Item UpdateItem(string desc, int price, string itemName, int id)
        {
            Item item = this.Items
                  .Where(i => i.ItemId == id).FirstOrDefault();
            item.Price = price;
            item.Desrciption = desc;
            item.ItemName = itemName;

            this.SaveChanges();
            return item;
        }
        public bool DeleteItem(int id)
        {
            try
            {
                Item item = this.Items
                            .Where(i => i.ItemId == id).FirstOrDefault();
                this.Entry(item).State = EntityState.Deleted;
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public IEnumerable<Item> Search(string itemName, int userid)
        {
            try
            {
                IEnumerable<Item> itemsByName = this.Items
                            .Where(i => i.ItemName.Contains(itemName) && i.UserId != userid).Include(i => i.User);
                return itemsByName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public IEnumerable<User> SearchUser(string userName, int userid)
        {
            try
            {
                IEnumerable<User> usersByName = this.Users
                            .Where(u => u.UserName.Contains(userName) && u.UserId != userid).Include(u => u.Items);
                return usersByName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Promote(int userid)
        {
            try
            {
                User user = this.Users
               .Where(u => u.UserId == userid)
               .FirstOrDefault();
                user.IsAdmin = true;
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool Block(int userid)
        {
            try
            {
               
                User user = this.Users
               .Where(u => u.UserId == userid).Include(u => u.Items)
               .FirstOrDefault();
                user.IsBlocked = true;
                foreach (Item item in user.Items)
                {
                    Items.Remove(item);
                }
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool Rate(int senderid, int ratedUserid, double rate)
        {
            try
            {

                User user = this.Users
               .Where(u => u.UserId == senderid).Include(u => u.RatingSenders)
               .FirstOrDefault();
                User user2 = this.Users
               .Where(u => u.UserId == ratedUserid)
               .FirstOrDefault();
                bool found = false;
                foreach (Rating r in user.RatingSenders)
                {
                    if(r.RatedUserId == ratedUserid)
                    {
                        found = true;
                        user2.SumRanks -= r.Rate;
                        user2.SumRanks += rate;                        
                        r.Rate = rate;
                    }
                }
                if (!found)
                {
                    user2.SumRanks += rate;
                    user2.CountRanked++;
                    Rating rating = new Rating
                    {
                        Rate = rate,
                        SenderId = senderid,
                        RatedUserId = ratedUserid
                    };
                    this.Ratings.Add(rating);
                }
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool Report(int senderid, int reportedUserid)
        {
            try
            {

                User user = this.Users
               .Where(u => u.UserId == senderid).Include(u => u.ReportSenders)
               .FirstOrDefault();
                User user2 = this.Users
               .Where(u => u.UserId == reportedUserid)
               .FirstOrDefault();
                bool found = false;
                foreach (Report r in user.ReportSenders)
                {
                    if (r.ReportedUserId == reportedUserid)
                    {
                        return false;
                    }
                }
                if (!found)
                {
                    Report report = new Report
                    {                      
                        SenderId = senderid,
                        ReportedUserId = reportedUserid
                    };
                    this.Reports.Add(report);
                    DailyReport dailyReport = this.DailyReports
               .Where(r => r.DayTime.Date == DateTime.Today)
               .FirstOrDefault();
                    MonthlyReport monthlyReport = this.MonthlyReports
                   .Where(r => r.DateOfMonth.Date.AddMonths(13) > DateTime.Today)
                   .FirstOrDefault();
                    dailyReport.ReportsNum++;
                    monthlyReport.ReportsNum++;
                }
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool Like(int senderid, int itemid)
        {
            try
            {

                User user = this.Users
               .Where(u => u.UserId == senderid).Include(u => u.LikedItems)
               .FirstOrDefault();
                bool found = false;
                foreach (LikedItem l in user.LikedItems)
                {
                    if (l.ItemId == itemid)
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    LikedItem like = new LikedItem
                    {
                        SenderId = senderid,
                        ItemId = itemid
                    };
                    this.LikedItems.Add(like);
                }
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool UnLike(int senderid, int itemid)
        {
            try
            {

                User user = this.Users
               .Where(u => u.UserId == senderid).Include(u => u.LikedItems)
               .FirstOrDefault();
               
                foreach (LikedItem l in user.LikedItems)
                {
                    if (l.ItemId == itemid)
                    {
                        this.LikedItems.Remove(l);
                    }
                }              
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
      
        public void AddMessage(string sender, string chatId, string message)
        {
            try
            {
                TextMessage m = new TextMessage
                {
                    SenderId = int.Parse(sender),
                    TextMessage1 = message,
                    ChatId = int.Parse(chatId),
                    SentTime = DateTime.Now
                };
                this.TextMessages.Add(m);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public TradeChat CreateGroup(TradeChat c)
        {
            try
            {
                this.Entry(c).State = EntityState.Added;
                this.SaveChanges();
                return c;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public TradeChat GetGroup(int chatId)
        {
            try
            {
                return this.TradeChats.Where(c => c.ChatId == chatId).Include(c => c.Buyer).Include(c => c.Seller).Include(c => c.Item).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public List<TradeChat> GetGroups(int accountID)
        {
            try
            {
                List<TradeChat> chats = this.TradeChats.Where(c => c.BuyerId == accountID || c.SellerId == accountID).Include(c => c.Buyer).Include(c => c.Seller).Include(c => c.Item).Include(c => c.TextMessages).ToList();               
                return chats;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public List<MonthlyReport> GetMonthlyReport() 
        {
            try
            {
                List<MonthlyReport> monthlyReport = this.MonthlyReports.Where(r => r.DateOfMonth.AddMonths(12) >= DateTime.Today).ToList();
                return monthlyReport;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public List<DailyReport> GetDailyReport()
        {
            try
            {
                DateTime minDate = DateTime.Today.AddDays(-7);
                List<DailyReport> dailyReport = this.DailyReports.Where(r => r.DayTime >= minDate).ToList();
                return dailyReport;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public List<Item>GetLikedItems(int userId)
        {
            try
            {
                List<LikedItem> likedItems = this.LikedItems.Where(l => l.SenderId==userId).Include(l => l.Item).Include(l => l.Item.User).ToList();
                List<Item> items = new List<Item>();
                foreach (var LikedItem in likedItems)
                {
                    items.Add(LikedItem.Item);
                }
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public void CreateMouthlyReport()
        {
            try
            {
                MonthlyReport m = new MonthlyReport();
                MonthlyReports.Add(m);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void CreateDailyReport()
        {
            try
            {
                DailyReport d = new DailyReport();
                DailyReports.Add(d);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public bool Sell(int sellerId, int buyerId, int itemId)
        {
            try
            {
                User seller = this.Users
                .Where(u => u.UserId == sellerId)
                .FirstOrDefault();
                User buyer = this.Users
                .Where(u => u.UserId == buyerId)
                .FirstOrDefault();
                Item item = this.Items
                .Where(i => i.ItemId == itemId).Include(i=>i.TradeChats)
                .FirstOrDefault();
                DailyReport dailyReport = this.DailyReports
               .Where(r => r.DayTime.Date == DateTime.Today)
               .FirstOrDefault();
                MonthlyReport monthlyReport = this.MonthlyReports
               .Where(r => r.DateOfMonth.Date.AddMonths(13) > DateTime.Today)
               .FirstOrDefault();
                dailyReport.ItemsDraded++;
                monthlyReport.ItemsTraded++;
                seller.Coins += item.Price;
                buyer.Coins -= item.Price;
                foreach (var chat in item.TradeChats)
                {
                    bool f=Regect(chat.ChatId);
                }
                this.Entry(item).State = EntityState.Deleted;
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool Regect(int chatId)
        {
            try
            {
                TradeChat chat = this.TradeChats
                               .Where(c => c.ChatId == chatId).Include(c=>c.TextMessages).FirstOrDefault();
                foreach (var textMessage in chat.TextMessages)
                {
                    this.Entry(textMessage).State = EntityState.Deleted;
                }
                this.Entry(chat).State = EntityState.Deleted;
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}