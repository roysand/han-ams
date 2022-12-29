using Application.Common.Interfaces;
using Domain.Entities;

namespace MqttReader.Console;

public class MqttBrokerConsumer : BackgroundService
{
    private readonly IMqttManagedClient _mqttManagedClient;
    private readonly IDetailRepository<Detail> _detailRepository;
    private readonly IConfig _config;

    public MqttBrokerConsumer(IMqttManagedClient mqttManagedClient
        ,IDetailRepository<Domain.Entities.Detail> detailRepository
        ,IConfig config)
    {
        _mqttManagedClient = mqttManagedClient;
        _detailRepository = detailRepository;
        _config = config;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mqttManagedClient.SubscribeAsync(_config.MqttConfig.MQTTTopic());
    }
}