using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Services;

namespace Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<Server>();
        }

        public static void AddApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILogger<Server>, Logger<Server>>();
            services.AddScoped<ILogger<ClientSession>, Logger<ClientSession>>();

        }
    }
}
