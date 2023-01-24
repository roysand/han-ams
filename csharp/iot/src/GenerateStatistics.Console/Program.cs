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
builder.Services.AddHostedService<GenerateStatisticsWorker>();

var app = builder.Build();

app.Run();