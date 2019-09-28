using System;
using System.Collections.Generic;
using System.Text;
using BusCache.Comandos.Models;
using BusCache.Comum.Collections;
using BusCache.Comum.Models;

namespace BusCache.Comandos.Services
{
    public class SendMessage
    {
        private readonly ComandoSMModel _comando;
        /// <summary>
        /// Comando de troca de mensagens entre serviços
        ///   sm <destino> "Mensagem a ser enviada"
        /// </summary>
        /// <param name="comando"></param>
        public SendMessage(ComandoSMModel comando)
        {
            _comando = comando;
        }
        
        public void Send()
        {
            _comando.Destino.SendData(_comando.Message);
        }
    }
}
