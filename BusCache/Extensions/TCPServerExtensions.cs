using BusCache.Comandos;
using BusCache.Comum.Collections;
using BusCache.Options;
using BusCache.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Extensions
{
    public static class TCPServerExtensions
    {
        public static IServiceCollection AddTCPServer(this IServiceCollection services, IConfiguration options)
        {
            services.Configure<TCPServerOptions>(options);
            services.AddSingleton<ServiceClientCollection>();
            services.AddTransient<Processor>();
            return services.AddSingleton<ITCPServer, TCPServer>();
        }
    }
}
