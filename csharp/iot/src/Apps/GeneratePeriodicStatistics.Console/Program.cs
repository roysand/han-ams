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

var configuration = new ConfigurationBuilder()
    .AddJsonFile("local.settings.json", optional:false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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

var cancellationToken = new CancellationToken(); 

GenerateStatistics(cmdLine);

GenerateMinutePowerUsageStatistics();


Console.WriteLine("Program ends");
return;

async void  GenerateMinutePowerUsageStatistics()
{
    await statRepository.GenerateMinutePowerUsageStatistics(cancellationToken);
}

void GenerateStatistics(CommandLineOptions commandLineOptions)
{
    switch (commandLineOptions.Service)
    {
        case ServiceType.Minute:
            GenerateMinutePowerUsageStatistics();
            break;
        
        case ServiceType.Hour:
            GenerateHourPowerUsageStatistics();
            break;
    }
}

async void  GenerateHourPowerUsageStatistics()
{
    await statRepository.GenerateHourPowerUsageStatistics(cancellationToken);
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