using System.IO;
using Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ServiceBusReadFunction.Startup))]

namespace ServiceBusReadFunction;

public class Startup : FunctionsStartup
{
    public Startup()
    {
        
    }
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("host.json", optional: true, reloadOnChange: true);

        IConfiguration configuration = configBuilder.Build();
        builder.Services.AddSingleton(configuration);
        builder.Services.AddInfrastructure(configuration);
    }
}