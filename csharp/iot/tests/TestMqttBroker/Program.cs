// See https://aka.ms/new-console-template for more information


using Application.Common.Interfaces;
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

// await TestMqttBroker.ClientSubscribeSamples.Subscribe_Topic();

var detailRepository = app.Services.GetService<IDetailRepository<Domain.Entities.Detail>>();
var client = new MqttManagedClient(detailRepository);

await ClientSubscribeSamples.Handle_Received_Application_Message();
//await client.SubscribeAsync("iot/ams", 1);

// Console.ReadKey();
// await client.Disconnect();

static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            services.AddInfrastructure(configuration);
        });

    return hostBuilder;
}
