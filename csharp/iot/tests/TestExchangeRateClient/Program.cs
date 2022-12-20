// See https://aka.ms/new-console-template for more information

using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Clients;
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
var exchangeRateRepository = app.Services.GetService<IExchangeRateRepository<ExchengeRate>>();
if (exchangeRateRepository == null)
{
    Console.WriteLine("Exchange rate repository is not found, program quits!!");
    return;
}

DateTime start = DateTime.Now, end = DateTime.Now;
if (args.Length == 2)
{
    DateTime.TryParse(args[0], out start);
    DateTime.TryParse(args[1], out end);
}
else
{
    Console.WriteLine("Missing commandline params. Need to dates 'yyyymmdd'");
    Console.WriteLine("Using data from database");

    var exRate = await exchangeRateRepository.FindNewestAsync();
    if (exRate == null)
    {
        start = DateTime.Now;
        end = start.AddDays(1);

    }
    else
    {
        start = exRate.ExchangeRatePeriod.AddDays(1);
        end = start.AddDays(1);
    }
}

var client = new WebApiClientExchangeRate(configuration);

var exchangeRates = (await client.DownloadExchangeRates(start, end));

exchangeRateRepository.AddRange(exchangeRates.ToList());

var count = await exchangeRateRepository.SaveChangesAsync(new CancellationToken());
Console.WriteLine($"Exchange rates added to the database is {count}");
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