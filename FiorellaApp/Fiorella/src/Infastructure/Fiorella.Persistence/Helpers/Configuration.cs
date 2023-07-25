using Microsoft.Extensions.Configuration;

namespace Fiorella.Persistence.Helpers;

internal static class Configuration
{
    internal static string ConnetionString { 
        get
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Directory.GetCurrentDirectory());
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("Default");
        }
    }
}
