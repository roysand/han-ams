// See https://aka.ms/new-console-template for more information

using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

var configBuilder = new ConfigurationBuilder()
    // .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("local.settings.json", optional:true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

IConfiguration configuration = configBuilder.Build();


IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<ITestInterface, TestClass>();
            services.AddHostedService<Worker>();
            services.AddApplication(configuration);
        });

var app = CreateHostBuilder(args).Build();
app.Run();

        
internal class Worker : BackgroundService
{
    public Worker(ITestInterface testClass)
    {
        testClass.Foo();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        StartAsync(stoppingToken);
        return Task.CompletedTask;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Hosted worker starts ...");
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Hosted worker stops ...");
        return Task.CompletedTask;
    }
}

interface ITestInterface
{
    void Foo();
}

class TestClass : ITestInterface
{
    public void Foo()
    {
        Console.WriteLine("From Foo in Testclass");
    }
}