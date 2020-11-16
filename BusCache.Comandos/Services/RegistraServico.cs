using BusCache.Comandos.Models;
using BusCache.Comum.Collections;
using BusCache.Comum.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos.Services
{
    public class RegistraServico : IComandosEntrada
    {
        private readonly ILogger<RegistraServico> _logger;
        private readonly ServiceClientCollection _collection;

        public RegistraServico(ILogger<RegistraServico> logger, ServiceClientCollection collection)
        {
            _logger = logger;
            _collection = collection;
        }
        public void Executar(ComandoModel comando, ServiceClient sender)
        {
            _logger.LogDebug($"Executando comando rg para [{sender.Name}]");
            _collection.UpdateName(sender, comando.Parametros);
            sender.SendData("Nome trocado com sucesso");
        }
    }
}
