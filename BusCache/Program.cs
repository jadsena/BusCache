﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusCache.Extensions;
using Serilog;
using BusCache.Comum.Extensions;

namespace BusCache
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTCPServer(hostContext.Configuration.GetSection("TCPServerOptions"));
                    services.AddCacheService();
                    services.ConfigureCommads(hostContext.Configuration.GetSection("ComandosEntrada"));
                    services.AddHostedService<HostService>();
                })
            .UseSerilog((hostContext, services, logger) =>
            {
                logger.ReadFrom.Configuration(hostContext.Configuration);
            });
    }
}
