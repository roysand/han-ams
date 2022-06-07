// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Net;
using System.Xml.Serialization;
using Application.Common.Helpers;
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


var client = new WebApiClientPrice(configuration);
var response = await client.GetPriceDayAhead();

var content = await response.Content.ReadAsStringAsync();

var serializer = new XmlSerializer(typeof(Publication_MarketDocument));
Publication_MarketDocument result;

using (TextReader reader = new StringReader(content))
{
    result = (Publication_MarketDocument)serializer.Deserialize(reader);
}

Console.WriteLine(result.ToString());
var price = result.CreatePrice();

var priceWithDetail = result.CreatePriceDetail();
Console.WriteLine(price.ToString());

Console.WriteLine($"Currency: {result.TimeSeries.currency_Unitname} Measure unit: {result.TimeSeries.price_Measure_Unitname}");


Console.WriteLine($"Currency: {result.TimeSeries.currency_Unitname} Measure unit: {result.TimeSeries.price_Measure_Unitname}");
var startDate = DateTime.Parse(result.periodtimeInterval.start).ToLocalTime();
var endDate = DateTime.Parse(result.periodtimeInterval.end).ToLocalTime();

Console.WriteLine($"StartDate: {startDate} EndDate: {endDate}");
Console.WriteLine(content);

var priceRepository = app.Services.GetService<IPriceRepository<Price>>();
priceRepository.Add(priceWithDetail);

await priceRepository.SaveChangesAsync(new CancellationToken());

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