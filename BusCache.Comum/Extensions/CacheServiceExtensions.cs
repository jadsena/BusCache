using BusCache.Comum.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusCache.Comum.Extensions
{
    public static class CacheServiceExtensions
    {
        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services.AddTransient<ICacheService, CacheService>();
        }
    }
}
