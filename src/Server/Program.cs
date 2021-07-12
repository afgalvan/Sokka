using System.Threading.Tasks;
using Core.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace Server
{
    internal static class Program
    {
        private static async Task Main(string[] args) =>
            await CreateHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                    new Startup(Configuration.Startup).ConfigureServices(services)
                );
    }
}
