// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<ITestInterface, TestClass>();
            services.AddHostedService<Worker>();
        });

var app = CreateHostBuilder(args).Build();
app.Run();

        
internal class Worker : IHostedService
{
    public Worker(ITestInterface testClass)
    {
        testClass.Foo();
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Hosted worker starts ...");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
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