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
            int countReports= user.ReportReportedUsers.Count;
            if (countReports > 40)
            {
                user.IsBlocked = true;
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
                Items.Remove(item);
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
               .Where(u => u.UserId == senderid).Include(u => u.ReportReportedUsers)
               .FirstOrDefault();
                User user2 = this.Users
               .Where(u => u.UserId == reportedUserid)
               .FirstOrDefault();
                bool found = false;
                foreach (Report r in user.ReportReportedUsers)
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
        public bool MakeALoan(Loan loan, int userid)
        {
            try
            {
                User user = this.Users
                  .Where(u => u.UserId == userid)
                  .FirstOrDefault();
                user.Loans.Add(loan);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public TextMessage AddMessage(TextMessage m)
        {
            try
            {
                this.TextMessages.Add(m);
                this.SaveChanges();
                return m;
            }
            catch
            {
                return null;
            }
        }
        public TradeChat CreateGroup(TradeChat c, int accountId)
        {
            try
            {
                this.TradeChats.Add(c);
                this.SaveChanges();
                return c;
            }
            catch
            {
                return null;
            }
        }
        public TradeChat GetGroup(int chatId)
        {
            try
            {
                return this.TradeChats.Where(c => c.ChatId == chatId).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public List<TradeChat> GetGroups(int accountID)
        {
            try
            {
                List<TradeChat> chats = this.TradeChats.Where(c => c.BuyerId == accountID || c.SellerId == accountID).ToList();               
                return chats;
            }
            catch
            {
                return null;
            }
        }
    }   
}