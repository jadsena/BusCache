using BusCache.Comandos.Models;
using BusCache.Comandos.Services;
using BusCache.Comum.Collections;
using BusCache.Comum.Models;
using BusCache.Comum.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos
{
    public class Processor
    {
        private readonly ILogger<Processor> _logger;
        private readonly ServiceClientCollection _collection;
        private readonly ICacheService _cacheService;

        public Processor(ILogger<Processor> logger, ServiceClientCollection collection, ICacheService cacheService )
        {
            _logger = logger;
            _collection = collection;
            _cacheService = cacheService;
        }

        public void Distribution(ComandoModel comando, ServiceClient sender)
        {
            _logger.LogDebug($"comando [{comando.Comando}], parametros [{comando.Parametros}].");
            switch (comando.Comando.ToLower())
            {
                case "rg":
                    _collection.UpdateName(sender, comando.Parametros);
                    sender.SendData("Nome trocado com sucesso");
                    break;
                case "sm":
                    ComandoSMModel comandoSM = ComandoSMModel.From(comando, sender, _collection.GetClientByName(comando.Parametros.Split(' ')[0]));
                    SendMessage sendMessage = new SendMessage(comandoSM);
                    sendMessage.Send();
                    break;
                case "ls":
                    StringBuilder sb = new StringBuilder();
                    IList<ServiceClient> list = string.IsNullOrWhiteSpace(comando.Parametros) ? _collection.GetAllClients() : _collection.GetClientsByName(comando.Parametros);
                    foreach (var item in list)
                    {
                        sb.AppendLine(item.Name);
                    }
                    sender.SendData(sb.ToString());
                    break;
                case "set":
                    string[] arr = comando.Parametros.Split(' ');
                    _cacheService.Set(arr[0], string.Join(" ", arr, 1, arr.Length - 1));
                    break;
                case "get":
                    var resp = _cacheService.Get(comando.Parametros);
                    sender.SendData(resp.ToString());
                    break;
                default:
                    sender.SendData($"Comando [{comando.Comando}] não identificado como um comando.");
                    break;
            }
        }
    }
}
