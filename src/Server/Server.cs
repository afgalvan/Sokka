using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreServer;
using Server.Services;

namespace Server
{
    public class Server : TcpServer, IHostedService
    {
        private readonly ILogger<Server>          _logger;
        private readonly ILogger<ClientSession>   _sessionLogger;
        private readonly IHostApplicationLifetime _appLifetime;

        public Server(SocketSetting config,
            IHostApplicationLifetime appLifetime,
            ILogger<Server> logger, ILogger<ClientSession> sessionLogger) :
            base(config.Host, config.Port)
        {
            _appLifetime   = appLifetime;
            _logger        = logger;
            _sessionLogger = sessionLogger;
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
            return new ClientSession(this, _sessionLogger);
        }

        protected override void OnError(SocketError error)
        {
            _logger.Log(LogLevel.Error,
                $"TCP server caught an error with code {error}");
        }
    }
}
