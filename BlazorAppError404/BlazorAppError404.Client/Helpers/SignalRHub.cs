using Microsoft.AspNetCore.SignalR.Client;
using SharedConfig;

namespace BlazorAppError404.Client.Helpers
{
    public class SignalRHub
    {
        public HubConnection? hubConnection = null;
        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;
        public List<string> Messages { get; set; } = [];

        public void InitHub()
        {
            Messages.Add("Setting hub connection");
            SetHubConnection();
            if (hubConnection != null)
            {
                hubConnection.On<string>(Config.ConnSuccessful, async (message) =>
                {
                    await Task.Delay(50);
                    Messages.Add($"Connexion message - {message}");
                });

                hubConnection.On<string>(Config.RandomMessageReceive, async (message) =>
                {
                    await Task.Delay(50);
                    Messages.Add($"Random message - {message}");
                });
            }
        }

        private void SetHubConnection()
        {
            string url = $"{Config.HubAddressHttps}/{Config.HubDefinition}";

            hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();
            hubConnection.ServerTimeout = new TimeSpan(0, 2, 0);
            Messages.Add(url);
            Messages.Add("Hubconnection has been set");
        }

        public async Task ConnectAsync()
        {
            try
            {
                Messages.Add("Connecting to the hub");
                if (hubConnection == null || IsConnected) return;
                await hubConnection.StartAsync();
                Messages.Add("Connected");
            }
            catch (Exception err)
            {
                Messages.Add($"Error message: {err.Message}");
                Messages.Add($"Inner exception: {err.InnerException}".ToString());
            }
        }

        public async Task ReceiveRandomMessage()
        {
            try
            {
                if (IsConnected && hubConnection != null)
                    await hubConnection.InvokeAsync("RandomMessage", "random");
                else
                    Messages.Add("Hub connection not established");
            }
            catch (Exception err)
            {
                Messages.Add($"Error message: {err.Message}");
                Messages.Add($"Inner exception: {err.InnerException}".ToString());
            }
        }
    }
}
