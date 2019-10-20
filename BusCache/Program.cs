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
using System.IO;
using BusCache.Comum.Extensions;
using System.Reflection;

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
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    string path = Path.GetDirectoryName(assembly.Location);
                    System.IO.Directory.SetCurrentDirectory(path);
                    configHost.SetBasePath(path);
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "DOTNETCORE_");
                })
                .ConfigureAppConfiguration((hostContext, appConfig) =>
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    string path = Path.GetDirectoryName(assembly.Location);
                    appConfig.SetBasePath(path);
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
                    services.AddCacheService();
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
