using BackgroundServices.Interface;
using Microsoft.AspNetCore.SignalR;

namespace BackgroundServices
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendOffersToUser(List<string> message)
        {
            Console.WriteLine(message);
            await Clients.All.SendOffersToUser(message);
        }

    }
}
