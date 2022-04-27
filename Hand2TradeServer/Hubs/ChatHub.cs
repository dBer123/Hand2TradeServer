using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;
using Hand2TradeServer.DTO;
using System;

namespace Hand2TradeServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string user, string message, string groupName)
        {
            IClientProxy proxy = Clients.Group(groupName);
            await proxy.SendAsync("ReceiveMessageFromGroup", user, message, groupName);
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



