using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Config;
using Application.Common.Models;
using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.Clients;

internal class AMSMqttManagedClient : MqttManagedClient, IMqttManagedClient
{
    public AMSMqttManagedClient(IConfig config) : base(config)
    {
    }

    public override Task Save(AMSReaderData data)
    {
        Console.WriteLine(JsonConvert.SerializeObject(data));
        
        return Task.CompletedTask;
    }
}