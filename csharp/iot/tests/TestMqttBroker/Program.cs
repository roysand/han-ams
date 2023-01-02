// See https://aka.ms/new-console-template for more information


using Application.Common.Interfaces;
using Application.Common.Interfaces.Config;
using Infrastructure;
using Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestMqttBroker;

Console.WriteLine("Hello, TestMqttBroker!");
var configuration = new ConfigurationBuilder()
    .AddJsonFile("local.settings.json", optional:true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var builder = CreateHostBuilder(args, configuration);
builder.ConfigureAppConfiguration(app => app.AddConfiguration(configuration));
var app = builder.Build();

await ClientSubscribeSamples.Handle_Received_Application_Message();

static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            services.AddInfrastructure();
        });

    return hostBuilder;
}
