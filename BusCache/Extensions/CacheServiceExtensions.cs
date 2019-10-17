using BusCache.Comum.Services;
using BusCache.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Extensions
{
    public static class CacheServiceExtensions
    {
        /// <summary>
        /// Adiciona o serviço de cache ao pool se serviços
        /// </summary>
        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services.AddTransient<ICacheService, CacheService>();
        }
    }
}
