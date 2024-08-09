using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SharedConfig;

namespace BlazorHub404
{
    class Program
    {
        #region Global Variables
        public static string OnlineLibrary { get; private set; }
        public static string CompanyLibrary_dir { get; private set; }
        public static string CL_InternalCom_dir { get; private set; }
        public static string CL_ActionRequest_dir { get; private set; }
        public static string SharePoint_dir { get; private set; }
        public static string Collaboration_dir { get; private set; }
        #endregion

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //.UseUrls($"{Config.HubAddressHttps}")
            .UseStartup<Startup>();
    }
}
