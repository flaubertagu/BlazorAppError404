namespace SharedConfig
{
    public class Config
    {
        public static string CorsDefinition { get; } = "websitecors";
        public static string HubDefinition { get; } = "hubs/chat";
        public static string HubAddressHttps { get; } = "https://localhost:7003";
        public static string HubAddressHttp { get; } = "http://localhost:5100";
        public static string ConnSuccessful { get; } = "ConnectionSuccessfull";
        public static string RandomMessageReceive { get; } = "RandomMessageReceive";
    }
}
