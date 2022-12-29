// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("local.settings.json", optional:true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var builder = CreateHostBuilder(args, configuration);
builder.ConfigureAppConfiguration(app => app.AddConfiguration(configuration));
var app = builder.Build();

Console.WriteLine("Hello, World!");
var statRepository = app.Services.GetService<IStatRepository<Domain.Entities.Detail>>();
if (statRepository == null)
{
     Console.WriteLine("No repository found!");
     return;
}

var result = await statRepository.DailyTotal(DateTime.Now.AddDays(-1), new CancellationToken());
Console.WriteLine(JsonSerializer.Serialize(result));

var daySum = await statRepository.GenerateDayStatistics(DateTime.Now.AddDays(-10), new CancellationToken());
Console.WriteLine(JsonSerializer.Serialize(daySum));

return; 

static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
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