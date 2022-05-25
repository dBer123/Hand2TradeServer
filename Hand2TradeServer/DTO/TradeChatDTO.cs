using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;

namespace Hand2TradeServer.DTO
{
    public class TradeChatDTO
    {
        public int ChatId { get; set; }
        public int ItemId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public bool IsTradeAgreed { get; set; }
      
        public virtual UserDTO Buyer { get; set; }
        public virtual ItemDTO Item { get; set; }
        public virtual UserDTO Seller { get; set; }
        public virtual ICollection<TextMessageDTO> TextMessages { get; set; }

       public TradeChatDTO()
        {

        }
        public TradeChatDTO(TradeChat chat)
        {
            this.ChatId = chat.ChatId;
            this.Item = new ItemDTO(chat.Item);
            this.ItemId = chat.ItemId;
            this.Buyer = new UserDTO(chat.Buyer, false);
            this.BuyerId = chat.BuyerId;
            this.Seller = new UserDTO(chat.Seller, false);
            this.SellerId = chat.SellerId;
            this.IsTradeAgreed = chat.IsTradeAgreed;
            this.TextMessages = new List<TextMessageDTO>();
            foreach (TextMessage message in chat.TextMessages)
            {
                this.TextMessages.Add(new TextMessageDTO(message));
            }


        }
    }
}
