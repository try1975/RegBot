using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.SpaServices.Extensions;

namespace CallNodeJsFromCore
{
    class Program
    {
        //https://medium.com/@michaelceber/running-typescript-and-javascript-using-node-js-in-net-core-ab2e8ba7ff4d
        static async Task<int> Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder()
                 .ConfigureServices((hostContext, services) =>
                 {
                     services.AddScoped<IJavaScriptService, JavaScriptService>();
                     services.AddLogging(configure => configure.AddConsole());
                     services.AddTransient<CallNodeJsFromCoreApp>();
                     services.AddNodeServices();
                 })
                 .ConfigureAppConfiguration((context, builder) =>
                 {
                     builder.AddJsonFile("CallNodeJsFromCore.json", optional: false);
                 })
                 .UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var configuration = services.GetRequiredService<IConfiguration>();
                //var threadCount = int.Parse(configuration["ThreadCount"]);

                var myService = services.GetRequiredService<CallNodeJsFromCoreApp>();
                try
                {
                    var result = await myService.Run();
                    Console.WriteLine(result);
                }
                catch (Exception exeption)
                {
                    Console.WriteLine($"{exeption}");
                }
            }

            return 0;
        }
    }
}
