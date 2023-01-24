// See https://aka.ms/new-console-template for more information

using GenerateStatistics.Console;
using Infrastructure;
using Infrastructure.Config;

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
builder.Services.AddSingleton<GenerateStatisticsWorker>();

builder.Services.AddHostedService(
    provider => provider.GetRequiredService<GenerateStatisticsWorker>());


var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.MapGet("/background", (
    GenerateStatisticsWorker service) =>
{
    return new PeriodicHostedServiceState(service.IsEnabled);
});

app.MapMethods("/background", new[] { "PATCH" }, (
    PeriodicHostedServiceState state, 
    GenerateStatisticsWorker service) =>
{
    service.IsEnabled = state.IsEnabled;
});

app.Run();