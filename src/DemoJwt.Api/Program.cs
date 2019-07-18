using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DemoJwt.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(urls: "http://localhost:10000")
                .Build();
    }
}
