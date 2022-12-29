using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureServiceBus.Startup))]
namespace AzureServiceBus;

public class Startup : FunctionsStartup
{
    public Startup()
    {
        
    }
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("host.json", optional: true, reloadOnChange: true);

        IConfiguration configuration = configBuilder.Build();
        builder.Services.AddSingleton(configuration);
        builder.Services.AddInfrastructure();
    }
}