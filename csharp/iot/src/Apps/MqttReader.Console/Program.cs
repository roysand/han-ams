using Infrastructure;
using Infrastructure.Config;
using MqttReader.Console;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

var config = new Config(builder.Configuration);

// Add services to the container.
builder.Services.AddSystemd();
builder.Services.AddInfrastructure();
builder.Services.AddHostedService<MqttBrokerConsumer>();

var app = builder.Build();
await app.RunAsync();