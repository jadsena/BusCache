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
        /// </summary>
        /// <param name="comando">comando a ser enviado</param>
        /// <example>sm [destino] "Mensagem a ser enviada"</example>
        public SendMessage(ComandoSMModel comando)
        {
            _comando = comando;
        }

        /// <summary>
        /// Entrega dados ao cliente
        /// </summary>
        /// <param name="Data">Dados a serem entregues</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        public void Send()
        {
            _comando.Destino.SendData(_comando.Message);
        }
    }
}
