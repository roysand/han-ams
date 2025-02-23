using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Config;
using Application.Common.Models;
using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.Clients;

internal class AMSMqttManagedClient : MqttManagedClient, IMqttManagedClient
{
    private readonly IConfig _config;
    private readonly IDetailRepository<Detail> _detailRepository;
    private int _counter = 0;

    public AMSMqttManagedClient(IConfig config
        , IDetailRepository<Detail> detailRepository) : base(config)
    {
        _config = config;
        _detailRepository = detailRepository;
        _counter = 0;
    }

    public override Task Save(AMSReaderData data)
    {
        if (data.Data == null)
        {
            return Task.CompletedTask;
        }
        
        var location = _config.ApplicationSettingsConfig.Location();
        _counter++;
        
        var detail = new Detail()
        {
            MeasurementId = Guid.NewGuid(),
            TimeStamp = data.TimeStamp,
            ObisCode = "1-0:1.7.0.255",
            Name = "Active power",
            ValueStr = "Active power",
            ObisCodeId = ObisCodeId.PowerUsed,
            Unit = "kW",
            Location = _config.ApplicationSettingsConfig.Location(),
            ValueNum = (decimal)data.Data.P / 1000
        };
        
        Console.WriteLine(
            $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.zzz")}:{JsonConvert.SerializeObject(detail)}");

        _detailRepository.Add(detail);
        
        if (_counter > _config.MqttConfig.MQTTDelayCountBeforeSaveToDb())
        {
            _counter = 0;
            _detailRepository.SaveChangesAsync(new CancellationToken());
        }
        return Task.CompletedTask;
    }
}