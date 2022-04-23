using System;
using System.Collections.Generic;
using System.Text;
using Hand2TradeServerBL.Models;

namespace Hand2TradeServer.DTO
{
    public class MsgDTO
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public string TextMessage1 { get; set; }
        public DateTime SentTime { get; set; }
        public virtual TradeChat Chat { get; set; }
        public virtual User Sender { get; set; }

      
    }
}

