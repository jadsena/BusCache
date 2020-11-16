using BusCache.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusCache
{
    public class HostService : BackgroundService
    {
        private ILogger<HostService> Logger { get; }
        private ITCPServer Server { get; }

        public HostService(ILogger<HostService> logger, ITCPServer server)
        {
            Logger = logger;
            Server = server;
        }
        
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Server.AceitarClientes(cancellationToken);
                return Task.CompletedTask;
            }
            catch (Exception ex) when (ex is ArgumentException ||
                                          ex is ArgumentNullException ||
                                          ex is ArgumentOutOfRangeException ||
                                          ex is OutOfMemoryException ||
                                          ex is OverflowException ||
                                          ex is EncoderFallbackException ||
                                          ex is InvalidOperationException ||
                                          ex is ObjectDisposedException ||
                                          ex is IOException)
            {
                Logger.LogError(ex.ToString());
                return Task.FromException(ex);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                Server.StopServer();
                return Task.CompletedTask;
            }
            catch(Exception ex) when (ex is ArgumentException ||
                                          ex is ArgumentNullException ||
                                          ex is ArgumentOutOfRangeException ||
                                          ex is OutOfMemoryException ||
                                          ex is OverflowException ||
                                          ex is EncoderFallbackException ||
                                          ex is InvalidOperationException ||
                                          ex is ObjectDisposedException ||
                                          ex is IOException)
            {
                Logger.LogError(ex.ToString());
                return Task.FromException(ex);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
