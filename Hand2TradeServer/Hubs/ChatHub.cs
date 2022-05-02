using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;
using Hand2TradeServer.DTO;
using System;
using Hand2TradeServer.Sevices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

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
        public async Task SendMessage(string sender, string receiver, string chatId, string message)
        {
            context.AddMessage(sender, chatId, message);
            await Clients.All.SendAsync("ReceiveMessage", sender, receiver, chatId, message);
            
        }

        public async Task OnConnect(string[] groupNames)
        {
            foreach (string groupName in groupNames)
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await base.OnConnectedAsync();
        }

        public async Task OnDisconnect(string[] groupNames)
        {
            foreach (string groupName in groupNames)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await base.OnDisconnectedAsync(null);
        }
    }
}



