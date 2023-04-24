// See https://aka.ms/new-console-template for more information

using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommandLine;
using GeneratePeriodicStatistics.Console;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("local.settings.json", optional:false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var builder = CreateHostBuilder(args, configuration);
builder.ConfigureAppConfiguration(app => app.AddConfiguration(configuration));
var app = builder.Build();

var cmdLine = new CommandLineOptions();
Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(parsed => cmdLine = parsed);

//or more simpler using Method group
Console.WriteLine("Hello, Generate Statistics Console!");

var statRepository = app.Services.GetService<IStatRepository<Detail>>();
if (statRepository == null)
{
    Console.WriteLine("Unable to load StatRepository");
    return;
}

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("App");

//app.Services.GetService<ILogger>();
if (logger == null)
{
    Console.WriteLine("Logger is null, program terminates");
    return;
}
var cancellationToken = new CancellationToken(); 

await GenerateStatistics(cmdLine);

Console.WriteLine("Program ends");

// Flush logger
Thread.Sleep(1);

return;

async Task<int>  GenerateMinutePowerUsageStatistics()
{
    var count = await statRepository.GenerateMinutePowerUsageStatistics(cancellationToken);
    return count;
}

async Task  GenerateStatistics(CommandLineOptions commandLineOptions)
{
    int count;
    switch (commandLineOptions.Service)
    {
        case ServiceType.Minute:
            count = await GenerateMinutePowerUsageStatistics();
            logger.LogInformation("Done Minute Power Usage Statistics ({Count})", count);
            break;
        
        case ServiceType.Hour:
            count = await GenerateHourPowerUsageStatistics();
            logger.LogInformation("Done Hour Power Usage Statistics ({Count})", count);
            break;
    }
}

async Task<int>  GenerateHourPowerUsageStatistics()
{
    var count = await statRepository.GenerateHourPowerUsageStatistics(cancellationToken);
    return count;
}

IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            // services.AddSingleton<ITemplatePostCodesRepository<TemplatePostCodes>, TemplatePostCodesRepository<TemplatePostCodes>>();
            services.AddInfrastructure();
        });

    return hostBuilder;
}