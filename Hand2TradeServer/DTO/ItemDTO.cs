using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;


namespace Hand2TradeServer.DTO
{
    public class ItemDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }       
        public string Desrciption { get; set; }       
        public virtual UserDTO User { get; set; }

        public ItemDTO() { }

        public ItemDTO(Item i)
        {
            Price = i.Price;
            Desrciption = i.Desrciption;
            ItemName = i.ItemName;
            ItemId = i.ItemId;
        }
    }
}
