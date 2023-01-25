// See https://aka.ms/new-console-template for more information

using Infrastructure;
using Infrastructure.Config;
using PeriodicStatisticsGeneratorService;

Console.WriteLine("Generate Statistics Console app starting ..");

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

var config = new Config(builder.Configuration);

// Add services to the container.
builder.Services.AddInfrastructure();

// Register as singleton first so it can be injected through Dependency Injection
builder.Services.AddSingleton<PeriodicStaticsGeneratorWorker>();

builder.Services.AddHostedService(
    provider => provider.GetRequiredService<PeriodicStaticsGeneratorWorker>());


var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.MapGet("/background", (
    PeriodicStaticsGeneratorWorker service) =>
{
    return new PeriodicStatisticsGeneratorServiceState(service.IsEnabled);
});

app.MapMethods("/background", new[] { "PATCH" }, (
    PeriodicStatisticsGeneratorServiceState state,
    PeriodicStaticsGeneratorWorker service) =>
{
    service.IsEnabled = state.IsEnabled;
});

app.Run();