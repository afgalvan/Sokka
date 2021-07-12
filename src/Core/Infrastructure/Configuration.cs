using System;
using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure
{
    public static class Configuration
    {
        public static IConfiguration Startup =>
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile(LoadConfigFile(), false, true)
                .Build();

        private static string GetEnvironment() =>
            Environment.GetEnvironmentVariable("ENVIRONMENT") != null
                ? Environment.GetEnvironmentVariable("ENVIRONMENT") + "."
                : "";

        private static string LoadConfigFile() =>
            $"appsettings.{GetEnvironment()}json";
    }
}
