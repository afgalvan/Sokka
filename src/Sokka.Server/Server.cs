using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Sokka.Server
{
    public class Server : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IConfiguration           _configuration;

        public Server(IHostApplicationLifetime appLifetime,
            IConfiguration configuration)
        {
            _appLifetime    = appLifetime;
            _configuration  = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(async () =>
                await Task.Run(() => { Console.WriteLine("Server started"); },
                    cancellationToken)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
