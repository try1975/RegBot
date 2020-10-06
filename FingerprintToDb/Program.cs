using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FingerprintToDb
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(configure => configure.AddConsole());
                    services.AddTransient<FingerprintToDbApplication>();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("FingerprintToDb.json", optional: false);
                })
                .UseConsoleLifetime();

            var host = builder.Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var myService = services.GetRequiredService<FingerprintToDbApplication>();
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
