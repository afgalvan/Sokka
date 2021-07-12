using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Sokka.Client
{
    public class App : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IConfiguration           _configuration;

        public App(IHostApplicationLifetime appLifetime,
            IConfiguration configuration)
        {
            _appLifetime   = appLifetime;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(async () =>
                await Task.Run(() =>
                    {
                    },
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
