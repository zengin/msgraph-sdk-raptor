using Microsoft.Extensions.Configuration;

namespace MsGraphSDKSnippetsCompiler
{
    public class AppSettings
    {
        public static IConfigurationRoot Config()
        {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }
    }
}