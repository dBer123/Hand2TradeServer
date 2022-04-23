using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;
using Hand2TradeServer.DTO;
using System;

namespace Hand2TradeServer.Hubs
{
    public class ChatHub : Hub
    {
        #region Add connection to the db context using dependency injection
        Hand2TradeDBContext context;
        public ChatHub(Hand2TradeDBContext context)
        {
            this.context = context;
        }
        #endregion

        public async Task SendMessage(MsgDTO message)
        {
            TextMessage msg = new TextMessage()
            {
                SentTime = message.SentTime,
                SenderId = message.SenderId,
                ChatId=message.ChatId,
                TextMessage1 = message.TextMessage1,

            };
            TextMessage returnedMsg = context.AddMessage(msg);
            if (returnedMsg != null)
                await Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}



