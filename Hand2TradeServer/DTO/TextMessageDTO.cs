using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;

namespace Hand2TradeServer.DTO
{
    public class TextMessageDTO
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public string TextMessage1 { get; set; }
        public DateTime SentTime { get; set; }
        public virtual TradeChatDTO Chat { get; set; }
        public virtual UserDTO Sender { get; set; }
        public TextMessageDTO() { }
        public TextMessageDTO(TextMessage message) 
        {
            this.ChatId = message.ChatId;
            this.SenderId = message.SenderId;
            this.SentTime = message.SentTime;
            this.TextMessage1 = message.TextMessage1;
            this.MessageId = message.MessageId;
        }

    }
}
