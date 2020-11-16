using BusCache.Comandos.Models;
using BusCache.Comum.Collections;
using BusCache.Comum.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos.Services
{
    public class ListaServicosRegistrados : IComandosEntrada
    {
        private readonly ILogger<ListaServicosRegistrados> _logger;
        private readonly ServiceClientCollection _collection;

        public ListaServicosRegistrados(ILogger<ListaServicosRegistrados> logger, ServiceClientCollection collection)
        {
            _logger = logger;
            _collection = collection;
        }

        public void Executar(ComandoModel comando, ServiceClient sender)
        {
            _logger.LogDebug($"Executando comando ls para [{sender.Name}]");
            StringBuilder sb = new StringBuilder();
            IList<ServiceClient> list = string.IsNullOrWhiteSpace(comando.Parametros) ? _collection.GetAllClients() : _collection.GetClientsByName(comando.Parametros);
            foreach (var item in list)
            {
                sb.AppendLine(item.Name);
            }
            sender.SendData(sb.ToString());
        }
    }
}
