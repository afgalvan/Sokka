using System;
using Microsoft.Extensions.Configuration;

namespace Sokka.Client.Settings
{
    public static class Configuration
    {
        public static IConfiguration Startup =>
            new ConfigurationBuilder()
                .AddJsonFile(LoadConfigFile(), false, true)
                .Build();

        public static string LoadConfigFile()
        {
#nullable enable
            string? args = Environment.GetEnvironmentVariable("ENVIRONMENT");
#nullable disable
            return $"appsettings.{(args == null ? "" : args + ".")}json";
        }
    }
}
