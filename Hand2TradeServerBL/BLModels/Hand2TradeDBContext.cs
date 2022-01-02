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
                .Where(u => u.Email == email && u.Passwrd == pswd).FirstOrDefault();

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

       public Item AddItem(int itemId ,int price, string description, string name, User user)
        {
            Item item = new Item
            {
                Desrciption = description,
                Price = price,
                ItemId = itemId,
                UserId = user.UserId,
                ItemName = name
            };
            User user1 = this.Users
               .Where(u => u.UserId == user.UserId).FirstOrDefault();
            user1.Items.Add(item);
            return item;
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
    }
}