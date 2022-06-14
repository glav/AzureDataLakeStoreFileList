using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeBlobFileList
{
    internal class Startup
    {
        public static ConfigItems Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", true);
            if (Environment.GetEnvironmentVariable("DEVELOPMENT") == "true")
            {
                configuration.AddJsonFile("appsettings-local.json", true);
            }

            var config = configuration.Build();
            var items = config.GetChildren();
            return new ConfigItems(config["BlobConnectionString"],config["ContainerName"], config.GetSection("DirectoriesToEnumerate").GetChildren().Select(x => x.Value).ToArray());
        }
    }
}
