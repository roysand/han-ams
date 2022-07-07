using Infrastructure;
using MqttReader.Console;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

IConfiguration configuration = configBuilder.Build();

// Add health endpoint
builder.WebHost.UseHealthEndpoints();
builder.WebHost.UseKestrel(options => options.AllowSynchronousIO = true);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddHostedService<MqttBrokerConsumer>();

var app = builder.Build();
app.UseHealthEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Dev");
}

app.UseHttpsRedirection();

app.Run();