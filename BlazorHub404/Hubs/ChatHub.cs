using Microsoft.AspNetCore.SignalR;
using SharedConfig;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHub404.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            try
            {
                await Clients.Client(Context.ConnectionId).SendAsync(Config.ConnSuccessful, $"User connected");
                await base.OnConnectedAsync();
            }
            catch (Exception)
            {
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task RandomMessage(string message)
        {
            string rndMsg = RandomString(20);
            await Clients.Client(Context.ConnectionId).SendAsync(Config.RandomMessageReceive, rndMsg);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
