using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using Newtonsoft.Json;

namespace Infrastructure.Clients
{
    public class MqttManagedClient : IMqttManagedClient
    {
        private readonly IDetailRepository<Detail> _detailRepository;
        private string ClientId = Guid.NewGuid().ToString();
        private string URI = "iot-phil4787";
        private string User = "iot";
        private string Password = "i3hYtte";
        private int Port = 1883;
        private bool UseTLS = false;
        private IManagedMqttClient _client;
        private readonly MqttFactory _factory;
        private int _counter = 0;
        public MqttManagedClient(IDetailRepository<Detail> detailRepository)
        {
            _detailRepository = detailRepository;
            _factory = new MqttFactory();
        }
        private async Task ConnectAsync()
        {
            var messageBuilder = new MqttClientOptionsBuilder()
                .WithClientId(ClientId)
                .WithCredentials(User, Password)
                .WithTcpServer(URI, Port)
                .WithCleanSession();

            var options = UseTLS
                ? messageBuilder
                    .WithTls()
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
                var amsDate =
                    JsonConvert.DeserializeObject<AMSReaderData>(System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload));
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.zzz")}{System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload)}");
                var detail = new Detail()
                {
                    MeasurementId = Guid.NewGuid(),
                    TimeStamp = amsDate.TimeStamp,
                    ObisCode = "1-0:1.7.0.255",
                    Name = "Active power",
                    ValueStr = "Active power",
                    ObisCodeId = ObisCodeId.PowerUsed,
                    Unit = "kW",
                    Location = "Pihl 4787",
                    ValueNum = (decimal)amsDate.Data.P / 1000
                };

                _detailRepository.Add(detail);
                _counter++;

                if (_counter > 9)
                {
                    await _detailRepository.SaveChangesAsync(new CancellationToken());
                    _counter = 0;
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
                .WithTopicFilter(f => { f.WithTopic("iot/ams"); })
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
    }
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        [JsonProperty("lv")]
        public string Lv { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("P")]
        public int P { get; set; }

        [JsonProperty("Q")]
        public int Q { get; set; }

        [JsonProperty("PO")]
        public int PO { get; set; }

        [JsonProperty("QO")]
        public int QO { get; set; }

        [JsonProperty("I1")]
        public double I1 { get; set; }

        [JsonProperty("I2")]
        public double I2 { get; set; }

        [JsonProperty("I3")]
        public double I3 { get; set; }

        [JsonProperty("U1")]
        public double U1 { get; set; }

        [JsonProperty("U2")]
        public double U2 { get; set; }

        [JsonProperty("U3")]
        public double U3 { get; set; }
    }

    public class AMSReaderData
    {
        public DateTime TimeStamp { get; } = DateTime.Now; 
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("up")]
        public int Up { get; set; }

        [JsonProperty("t")]
        public int T { get; set; }

        [JsonProperty("vcc")]
        public double Vcc { get; set; }

        [JsonProperty("rssi")]
        public int Rssi { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}