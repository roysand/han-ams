using Application.Common.Interfaces;
using Domain.Entities;

namespace MqttReader.Console;

public class MqttBrokerConsumer : BackgroundService
{
    private readonly IMqttManagedClient _mqttManagedClient;
    private readonly IDetailRepository<Detail> _detailRepository;
    private readonly IConfiguration _configuration;

    public MqttBrokerConsumer(IMqttManagedClient mqttManagedClient
        ,IDetailRepository<Domain.Entities.Detail> detailRepository
        ,IConfiguration configuration)
    {
        _mqttManagedClient = mqttManagedClient;
        _detailRepository = detailRepository;
        _configuration = configuration;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mqttManagedClient.SubscribeAsync("iot/ams");
    }
}