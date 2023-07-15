using Microsoft.AspNetCore.SignalR;

namespace ChatApplicationSignalR
{
    public class ChatHub : Hub
    {
        public async Task SendToAllClients(string username, string message)
        {
            await Clients.All.SendAsync("ReciveMessage", username, message);
        }

        public async Task SendToSpecificClient(string username, string message)
        {
            await Clients.User(username).SendAsync(message);
        }
    }
}
