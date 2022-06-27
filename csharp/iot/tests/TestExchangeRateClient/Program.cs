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
var client = new WebApiClientExchangeRate(configuration, exchangeRateRepository);

var exchangeRates = await client.DownloadExchangeRates();

foreach (var exchangeRate in exchangeRates)
{
    exchangeRateRepository.Add(exchangeRate);
}

await exchangeRateRepository.SaveChangesAsync(new CancellationToken());
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
            services.AddInfrastructure(configuration);
        });

    return hostBuilder;
}