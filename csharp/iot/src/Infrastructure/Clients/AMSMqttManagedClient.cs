using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Config;
using Application.Common.Models;
using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.Clients;

internal class AMSMqttManagedClient : MqttManagedClient
{
    private readonly IConfig _config;
    private readonly IDetailRepository<Detail> _detailRepository;
    private int _counter;

    public AMSMqttManagedClient(IConfig config
        , IDetailRepository<Detail> detailRepository) : base(config)
    {
        _config = config;
        _detailRepository = detailRepository;
        _counter = 0;
    }

    public override async Task Save(AMSReaderData data)
    {
        if (data == null)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.zzz} Ignoring MQTT message because deserialization returned null.");
            return;
        }

        if (data.Data?.P == null)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.zzz} Ignoring MQTT message because data.P is missing.");
            return;
        }

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
            ValueNum = (decimal)data.Data.P.Value / 1000
        };
        
        Console.WriteLine(
            $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.zzz")}:{JsonConvert.SerializeObject(detail)}");

        _detailRepository.Add(detail);
        
        if (_counter > _config.MqttConfig.MQTTDelayCountBeforeSaveToDb())
        {
            _counter = 0;
            await _detailRepository.SaveChangesAsync(new CancellationToken());
        }
    }
}