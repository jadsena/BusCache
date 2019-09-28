using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace BusCache.Comum.Models
{
    public class ServiceClient
    {
        public string ServiceName { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        public string Name { get; set; }
        public TcpClient Client { get; set; }
        public List<string> Channels { get; set; } = new List<string>();

        /// <summary>
        /// Entrega dados ao cliente
        /// </summary>
        /// <param name="Data">Dados a serem entregues</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        public void SendData(string Data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(Data + Environment.NewLine);
            NetworkStream stream = Client.GetStream();

            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
