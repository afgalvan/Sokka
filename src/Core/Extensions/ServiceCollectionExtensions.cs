using System;
using System.Globalization;
using Core.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddSocketSettings(
            this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection serverConfig =
                configuration.GetSection("Server");

            string host = serverConfig["Host"];
            var    port = Convert.ToInt32(serverConfig["Port"], CultureInfo.CurrentCulture);

            services.AddScoped(_ => new SocketSetting(host, port));
        }
    }
}
