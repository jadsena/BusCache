using BusCache.Comandos.Models;
using BusCache.Comum.Collections;
using BusCache.Comum.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos.Services
{
    public class EnviaMensagem : IComandosEntrada
    {
        private readonly ILogger<EnviaMensagem> _logger;
        private readonly ServiceClientCollection _collection;

        public EnviaMensagem(ILogger<EnviaMensagem> logger, ServiceClientCollection collection)
        {
            _logger = logger;
            _collection = collection;
        }
        public void Executar(ComandoModel comando, ServiceClient sender)
        {
            ComandoSMModel comandoSM = ComandoSMModel.From(comando, sender, _collection.GetClientByName(comando.Parametros.Split(' ')[0]));
            _logger.LogDebug($"Executando comando sm de [{sender.Name}] para [{comandoSM.Destino.Name}]");
            SendClientMessage sendMessage = new SendClientMessage(comandoSM);
            sendMessage.Send();
        }
    }
}
