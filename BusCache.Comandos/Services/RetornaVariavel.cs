using BusCache.Comandos.Models;
using BusCache.Comum.Models;
using BusCache.Comum.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos.Services
{
    public class RetornaVariavel : IComandosEntrada
    {
        private readonly ILogger<RetornaVariavel> _logger;
        private readonly ICacheService _cacheService;

        public RetornaVariavel(ILogger<RetornaVariavel> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public void Executar(ComandoModel comando, ServiceClient sender)
        {
            _logger.LogDebug($"Executando comando get para [{sender.Name}]");
            var resp = _cacheService.Get(comando.Parametros);
            sender.SendData(resp.ToString());
            _logger.LogInformation($"Send [{resp}] to [{sender.Name}].");
        }
    }
}
