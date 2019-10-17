using BusCache.Comum.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusCache.Comum.Extensions
{
    public static class CacheServiceExtensions
    {
        /// <summary>
        /// Adiciona o serviço de cache ao pool de serviços
        /// </summary>
        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services.AddTransient<ICacheService, CacheService>();
        }
    }
}
