using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Config;
using Application.Common.Models;
using Domain.Entities;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using Newtonsoft.Json;

namespace Infrastructure.Clients
{
    public abstract class MqttManagedClient : IMqttManagedClient
    {
        private readonly IConfig _config;
        private readonly string _clientId = System.Net.Dns.GetHostName() + "-" + Guid.NewGuid().ToString().Substring(0,8);
        private IManagedMqttClient _client;
        private readonly MqttFactory _factory;
        public MqttManagedClient(IConfig config)
        {
            _config = config;
            _factory = new MqttFactory();
        }
        private async Task ConnectAsync()
        {
            var messageBuilder = new MqttClientOptionsBuilder()
                .WithClientId(_clientId)
                .WithCredentials(_config.MqttConfig.MQTTUserName(), _config.MqttConfig.MQTTUserPassword())
                .WithTcpServer(_config.MqttConfig.MQTTServerURI(), _config.MqttConfig.MQTTServerPortNr())
                .WithCleanSession();
            
            var options = _config.MqttConfig.MQTTUseTLS()
                ? messageBuilder
                    .WithTlsOptions(o => o.WithSslProtocols(SslProtocols.Tls13))
                    .Build()
                : messageBuilder
                    .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();

            _client = _factory.CreateManagedMqttClient();

            _client.ConnectedAsync += e =>
            {
                Console.WriteLine("Application is connected to broker");
                return Task.CompletedTask;
            };

            _client.DisconnectedAsync += e =>
            {
                Console.WriteLine("Appliction is disconnected from broker");
                return Task.CompletedTask;
            };

            _client.ApplicationMessageReceivedAsync += async e =>
            {
                try
                {
                    var amsReaderData =
                        JsonConvert.DeserializeObject<AMSReaderData>(System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload));

                    await Save(amsReaderData);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    //throw;
                }
            };
            
            await _client.StartAsync(managedOptions);
        }

        public async Task SubscribeAsync(string topic, int qos = 1)
        {
            if (_client == null || !_client.IsConnected)
            {
                await ConnectAsync();
            }
            
            var mqttSubscribeOptions = _factory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic); })
                .Build();

            await _client.SubscribeAsync(topic, (MqttQualityOfServiceLevel)qos);
        }

        public async Task UnSubscribeAsync(string topic)
        {
            await _client.UnsubscribeAsync(topic);
        }

        public async Task Disconnect()
        {
            await _client.StopAsync();
        }

        public virtual async Task Save(AMSReaderData data)
        {
            Console.WriteLine(
                $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.zzz")}{JsonConvert.SerializeObject(data)}");
                
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
        
            //Console.WriteLine(JsonConvert.SerializeObject(detail));
        
            
            await Task.CompletedTask;
        }
    }
}