using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;

namespace Hand2TradeServer.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Passwrd { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public int Coins { get; set; }
        public DateTime BirthDate { get; set; }
        public string Adress { get; set; }
        public bool IsBlocked { get; set; }
        public string CreditNum { get; set; }
        public string CVV { get; set; }
        public DateTime CardDate { get; set; }
        public int SumRanks { get; set; }
        public int CountRanked { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public UserDTO() { }



        public UserDTO(User u)
        {
            UserId = u.UserId;
            Email = u.Email;
            Passwrd = u.Passwrd;
            UserName = u.UserName;
            IsAdmin = u.IsAdmin;
            Coins = u.Coins;
            BirthDate = u.BearthDate;
            Adress = u.Adress;
            IsBlocked = u.IsBlocked;
            CreditNum = u.CreditCardNumber;
            CVV = u.Cvv;
            CardDate = u.CreditCardValidity;
            SumRanks = u.SumRanks;
            CountRanked = u.CountRanked;
            Items = u.Items;
        }

    }
}
