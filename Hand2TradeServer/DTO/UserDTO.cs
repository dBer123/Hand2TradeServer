﻿using System;
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
        public double SumRanks { get; set; }
        public int CountRanked { get; set; }
        public DateTime JoinedDate { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<TradeChatDTO> TradeChatBuyers { get; set; }
        public virtual ICollection<TradeChatDTO> TradeChatSellers { get; set; }

        public UserDTO() { }
        


        public UserDTO(User u, bool isIncludeChats)
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
            SumRanks = u.SumRanks;
            CountRanked = u.CountRanked;
            Items = u.Items;
            JoinedDate = u.JoinedDate;
            if (isIncludeChats)
            {
                foreach (TradeChat chat in u.TradeChatBuyers)
                {
                    this.TradeChatBuyers.Add(new TradeChatDTO(chat));
                }
                foreach (TradeChat chat in u.TradeChatSellers)
                {
                    this.TradeChatSellers.Add(new TradeChatDTO(chat));
                }
            }            
        }

    }
}
