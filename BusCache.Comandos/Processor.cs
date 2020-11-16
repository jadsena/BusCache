using BusCache.Comandos.Models;
using BusCache.Comandos.Services;
using BusCache.Comum.Collections;
using BusCache.Comum.Models;
using BusCache.Comum.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos
{
    public class Processor
    {
        private readonly ILogger<Processor> _logger;
        private readonly IServiceProvider _provider;
        private readonly ComandosEntradaCollection _comandosEntrada;

        public Processor(ILogger<Processor> logger, IServiceProvider provider, IOptions<ComandosEntradaCollection> options)
        {
            _logger = logger;
            _provider = provider;
            _comandosEntrada = options.Value;
        }
        /// <summary>
        /// Roteador de comandos recebidos
        /// </summary>
        /// <param name="comando">Comando recebido</param>
        /// <param name="sender">Cliente que enviou o comando</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public void Distribution(ComandoModel comando, ServiceClient sender)
        {
            _logger.LogDebug($"comando [{comando.Comando}], parametros [{comando.Parametros}].");
            if (!_comandosEntrada.Contains(comando.Comando.ToLower()))
            {
                sender.SendData($"Comando [{comando.Comando}] não identificado como um comando.");
                return;
            }
            string srv = _comandosEntrada[comando.Comando.ToLower()].Classe;
            IComandosEntrada entrada = (IComandosEntrada)_provider.GetService(Type.GetType(srv));
            entrada.Executar(comando, sender);
        }
    }
}
