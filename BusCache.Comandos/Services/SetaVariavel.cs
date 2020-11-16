using BusCache.Comandos.Models;
using BusCache.Comum.Models;
using BusCache.Comum.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos.Services
{
    public class SetaVariavel : IComandosEntrada
    {
        private readonly ILogger<SetaVariavel> _logger;
        private readonly ICacheService _cacheService;

        public SetaVariavel(ILogger<SetaVariavel> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public void Executar(ComandoModel comando, ServiceClient sender)
        {
            _logger.LogDebug($"Executando comando set para [{sender.Name}]");
            string[] arr = comando.Parametros.Split(' ');
            _cacheService.Set(arr[0], string.Join(" ", arr, 1, arr.Length - 1));
            sender.SendData("Valor armezenado com sucesso.");
        }
    }
}
