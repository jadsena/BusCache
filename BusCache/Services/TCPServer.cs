﻿using BusCache.Comum.Collections;
using BusCache.Comandos.Models;
using BusCache.Comum.Models;
using BusCache.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusCache.Comandos;

namespace BusCache.Services
{
    public class TCPServer : IDisposable, ITCPServer
    {
        private bool disposed = false;
        private object _lock = new object();
        private TCPServerOptions TcpServerOptions { get; }
        private ILogger Logger { get; }
        private TcpListener Listener { get; }
        private ServiceClientCollection Collection { get; }

        private Processor _processor;

        private List<Task> Tasks { get; }
        private readonly CancellationTokenSource cts;
        public TCPServer(ILogger<TCPServer> logger, ServiceClientCollection clients, IOptions<TCPServerOptions> options, Processor processor)
        {
            Logger = logger;
            TcpServerOptions = options.Value;
            Collection = clients;
            _processor = processor;
            Tasks = new List<Task>();
            cts = new CancellationTokenSource();
            IPAddress address = IPAddress.Parse(TcpServerOptions.IP);
            Listener = new TcpListener(address, TcpServerOptions.Port);
            Logger.LogInformation($"Iniciando abertura de ip:porta tcp: {TcpServerOptions.IP}:{TcpServerOptions.Port}");
            Listener.Start();
        }
        public Task AceitarClientes(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    Logger.LogInformation($"Waiting for client in ip:port tcp: {TcpServerOptions.IP}:{TcpServerOptions.Port}...");
                    var clientTask = await Listener.AcceptTcpClientAsync(); // Get the client
                    ServiceClient client = new ServiceClient() { Client = clientTask, ServiceName = Guid.NewGuid().ToString(), Name = $"Servico{Collection.Count + 1}" };
                    Collection.Add(client);
                    Logger.LogDebug($"Someone connected!!! {client.Name}");
                    Task task = new Task(async () => await Handle_clients(client.ServiceName), cts.Token);
                    task.Start();
                    Tasks.Add(task);
                }
            }, cancellationToken);
        }

        public void StopServer()
        {
            cts.Cancel();
            foreach (var item in Collection)
            {
                item.Client.Close();
                item.Client.Dispose();
            }
        }

        private async Task Handle_clients(object o)
        {
            string ClientName = o.ToString();
            ServiceClient clients;

            lock (_lock) clients = Collection[ClientName];
            TcpClient client = clients.Client;
            string message = "";
            NetworkStream stream = null;
            StreamReader sr = null;
            while ((message != null && !message.StartsWith("quit")))
            {
                stream = client.GetStream();
                clients = Collection[ClientName];

                try
                {
                    sr = new StreamReader(stream);
                    message = await sr.ReadLineAsync();
                }
                catch (IOException ex)
                {
                    Logger.LogError($"received ERROR from [{clients.Name}]:[{clients.Client.Client.RemoteEndPoint}]");
                    Logger.LogError(ex.ToString());
                    break;
                }
                if (string.IsNullOrWhiteSpace(message))
                {
                    break;
                }

                //limpa final da mensagem
                if (message.EndsWith("\n")) message = message.Substring(0, message.Length - 1);
                Logger.LogDebug($"received message [{message}] from [{clients.Name}]:[{clients.Client.Client.RemoteEndPoint}]");
                if (message.StartsWith("quit")) break;
                try
                {
                    ComandoModel comando = ComandoModel.Parse(message);
                    _processor.Distribution(comando, clients);
                }
                catch (Exception ex)
                {
                    clients.SendData(ex.ToString());
                    Logger.LogError(ex.ToString());
                }
            }

            Logger.LogDebug($"Closing connection [{ClientName}] Naming [{clients.Name}].");
            lock (_lock)
            {
                Collection.Remove(clients);
            }
            client.Client.Shutdown(SocketShutdown.Both);
            client.Client.Disconnect(true);
            stream?.Dispose();
            sr?.Dispose();
            client.Close();
            client.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
                if (!disposed)
                {
                    Listener.Stop();
                    disposed = true;
                }
        }
    }
}