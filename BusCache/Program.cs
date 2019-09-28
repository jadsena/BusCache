using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;
using BusCache.Extensions;
using System.Threading.Tasks;
using Serilog;

namespace BusCache
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var builder = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Environment.CurrentDirectory);
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "DOTNETCORE_");
                })
                .ConfigureAppConfiguration((hostContext, appConfig) =>
                {
                    appConfig.AddJsonFile("appsettings.json", false, true);
                    appConfig.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(hostContext.Configuration)
                        .CreateLogger();
                    services.AddHostedService<HostService>();
                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.AddSerilog();
                    });
                    services.AddOptions();
                    services.AddTCPServer(hostContext.Configuration.GetSection("TCPServerOptions"));
                })
                .UseConsoleLifetime();

            if (isService)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}
