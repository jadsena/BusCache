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
        //services.AddMemoryCache();
        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services.AddTransient<ICacheService, CacheService>();
        }
    }
}
