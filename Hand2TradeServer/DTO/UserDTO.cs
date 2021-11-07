﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;

namespace Hand2TradeServer.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Passwrd { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public int Coins { get; set; }
        public int TotalRank { get; set; }
        public DateTime BirthDate { get; set; }
        public string Adress { get; set; }
        public bool IsBlocked { get; set; }
        public string CreditNum { get; set; }
        public string CVV { get; set; }
        public DateTime CardDate { get; set; }





        public UserDTO(User u)
        {
            Email = u.Email;
            Passwrd = u.Passwrd;
            UserName = u.UserName;
            IsAdmin = u.IsAdmin;
            Coins = u.Coins;
            TotalRank = u.TotalRank;
            BirthDate = u.BearthDate;
            Adress = u.Adress;
            IsBlocked = u.IsBlocked;
            CreditNum = u.CreditCardNumber;
            CVV = u.Cvv;
            CardDate = u.CreditCardValidity;
            
        }

    }
}