using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreServer;
using Server.Services;

namespace Server
{
    public class Server : TcpServer, IHostedService
    {
        private readonly ILogger<Server>          _logger;
        private readonly IServiceProvider         _serviceProvider;
        private readonly IHostApplicationLifetime _appLifetime;

        public Server(SocketSetting config,
            IHostApplicationLifetime appLifetime,
            ILogger<Server> logger, IServiceProvider serviceProvider) :
            base(config.Host, config.Port)
        {
            _appLifetime     = appLifetime;
            _logger          = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(async () =>
                await Task.Run(() =>
                    {
                        bool isRunning = Start();
                        if (isRunning)
                        {
                            _logger.Log(LogLevel.Information,
                                "Server started...");
                        }
                    },
                    cancellationToken)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override TcpSession CreateSession()
        {
            var sessionLogger = _serviceProvider.GetRequiredService<ILogger<ClientSession>>();
            return new ClientSession(this, sessionLogger);
        }

        protected override void OnError(SocketError error)
        {
            _logger.Log(LogLevel.Error,
                $"TCP server caught an error with code {error}");
        }
    }
}
