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
        
        public async Task SendMessageToGroup(string user, string message, string groupName)
        {
            IClientProxy proxy = Clients.Group(groupName);
            await proxy.SendAsync("ReceiveMessageFromGroup", user, message, groupName);
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
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



