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
using MQTTnet.Packets;
using MQTTnet.Protocol;
using Newtonsoft.Json;

namespace Infrastructure.Clients
{
    public abstract class MqttManagedClient : IMqttManagedClient
    {
        private readonly IConfig _config;
        private readonly string _clientId = System.Net.Dns.GetHostName() + "-" + Guid.NewGuid().ToString().Substring(0,8);
        private IMqttClient _client;
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
            _client = _factory.CreateMqttClient();

            _client.ConnectedAsync += e =>
            {
                Console.WriteLine("Application is connected to broker");
                return Task.CompletedTask;
            };

            _client.ApplicationMessageReceivedAsync += async args =>
            {
                try
                {
                    var amsReaderData =
                        JsonConvert.DeserializeObject<AMSReaderData>(System.Text.Encoding.Default.GetString(args.ApplicationMessage.PayloadSegment.ToArray()));

                    await Save(amsReaderData);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    //throw;
                }
            };
            
            await _client.ConnectAsync(options, CancellationToken.None);
            
            // Set up disconnection handling and automatic reconnection
            _client.DisconnectedAsync += async e =>
            {
                Console.WriteLine("Application is disconnected from broker");
                Console.WriteLine("Attempting to reconnect...");
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await _client.ConnectAsync(options, CancellationToken.None);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Reconnection failed: {exception.Message}");
                }
            };
        }

        public async Task SubscribeAsync(string topic, int qos = 1)
        {
            if (_client == null || !_client.IsConnected)
            {
                await ConnectAsync();
            }
            
            var mqttSubscribeOptions = _factory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)qos); })
                .Build();

            await _client.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        public async Task UnSubscribeAsync(string topic)
        {
            await _client.UnsubscribeAsync(new MqttClientUnsubscribeOptions { TopicFilters = { topic } }, CancellationToken.None);
        }

        public async Task Disconnect()
        {
            await _client.DisconnectAsync(new MqttClientDisconnectOptions(), CancellationToken.None);
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