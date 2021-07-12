using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Sokka.Client.Settings;

namespace Sokka.Client
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
