using BusCache.Comum.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comum.Extensions
{
    public static class ComandosEntradaExtensions
    {
        public static IServiceCollection ConfigureCommads(this IServiceCollection services, IConfiguration configuration)
        {
            ComandosEntradaCollection entradaModels = configuration.Get<ComandosEntradaCollection>();
            foreach (var item in entradaModels)
            {
                services.AddTransient(Type.GetType(item.Classe));
            }
            return services.Configure<ComandosEntradaCollection>(configuration);
        }
    }
}
