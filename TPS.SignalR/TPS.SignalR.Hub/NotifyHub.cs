using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TPS.SignalR.Hub
{
    public class NotifyHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public Task SendMessageTOAll(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }
        public Task JoinGroup(string userId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        public Task SendMessage(string userId, string message)
        {
            return Clients.Group(userId).SendAsync("ReceiveMessage", message);
        }
    }
}
