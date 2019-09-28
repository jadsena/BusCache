using BusCache.Comum.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos.Models
{
    public class ComandoSMModel : ComandoModel
    {
        private string message;
        public string Message
        {
            get
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    var pos = Parametros.IndexOf('"');
                    message = Parametros.Substring(pos + 1, Parametros.Length - pos - 2);
                }
                return message;
            }
        }
        public ServiceClient Origem { get; internal set; }
        public ServiceClient Destino { get; internal set; }
        public static ComandoSMModel From(ComandoModel model, ServiceClient origem, ServiceClient destino)
        {
            return new ComandoSMModel
            {
                Comando = model.Comando,
                Destino = destino,
                Origem = origem,
                Parametros = model.Parametros
            };
        }
    }
}
