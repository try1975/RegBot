using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FingerprintDownloader
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(configure => configure.AddConsole());
                    services.AddHttpClient();
                    services.AddTransient<Downloader>();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("Custom.json", optional: false);
                })
                .UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var configuration = services.GetRequiredService<IConfiguration>();
                var threadCount = Int32.Parse(configuration["ThreadCount"]);

                var myService = services.GetRequiredService<Downloader>();
                for (int i = 0; i < threadCount; i++)
                {
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
            }

            return 0;
        }
    }
}
