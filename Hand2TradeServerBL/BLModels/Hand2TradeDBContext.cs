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

            return user;
        }
        public User AddUser(string password, string username, string email, int coins, string adress, DateTime dDay, bool isAdmin, bool isBlocked, string creditNum, DateTime cardDate, string cvv )
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
            active.CreditCardNumber = creditNum;
            active.CreditCardValidity = cardDate;
            active.Cvv = cvv;

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
    }
}