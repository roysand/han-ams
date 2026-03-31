
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestMqttBroker;

public static class ClientSubscribeSamples
{
public static async Task Handle_Received_Application_Message()
{
    var mqttFactory = new MqttFactory();

    using var mqttClient = mqttFactory.CreateMqttClient();

    var startedAt = DateTime.Now;
    var fileName = $"values-{startedAt:yyyyMMdd-HHmmss}.txt";
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

    await using var writer = new StreamWriter(filePath, append: true);
    writer.AutoFlush = true;
    await writer.WriteLineAsync("timestamp\tp");

    // Async-safe gate for concurrent MQTT callbacks.
    var writeGate = new SemaphoreSlim(1, 1);

    var mqttClientOptions = new MqttClientOptionsBuilder()
        .WithTcpServer("iot-hytta")
        .WithCredentials("mqtt-ams", "mqtt-ams")
        .WithClientId("test_client_id-")
        .Build();

    mqttClient.ApplicationMessageReceivedAsync += async e =>
    {
        var payload = System.Text.Encoding.Default.GetString(e.ApplicationMessage.PayloadSegment.ToArray());

        Console.WriteLine($"{DateTime.Now} - Received application message.");
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {payload}");

        try
        {
            var json = JObject.Parse(payload);
            int? p = json["data"]?["P"]?.Value<int>();

            if (p.HasValue)
            {
                var rowTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var line = $"{rowTimestamp}\t{p.Value}";

                await writeGate.WaitAsync();
                try
                {
                    await writer.WriteLineAsync(line);
                }
                finally
                {
                    writeGate.Release();
                }

                Console.WriteLine($"Parsed data.P = {p.Value} (written to {fileName})");
            }
            else
            {
                Console.WriteLine("JSON parsed, but data.P was missing.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse payload JSON: {ex.Message}");
        }
    };

    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

    var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
        .WithTopicFilter(f => { f.WithTopic("iot/ams"); })
        .Build();

    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

    Console.WriteLine("MQTT client subscribed to topic.");
    Console.WriteLine($"Writing values to: {filePath}");
    Console.WriteLine("Press enter to exit.");
    Console.ReadLine();
    Console.WriteLine(".... stopping");
}


    public static async Task Subscribe_Topic()
    {
        /*
         * This sample subscribes to a topic.
         */

        var mqttFactory = new MqttFactory();

        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("822b669a3fd14b2f818fd40ea11bbaaa.s2.eu.hivemq.cloud")
                .WithCredentials("iot_sandaas","i3hYtten")
                .WithTlsOptions(o => o.UseTls())
                .Build();

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic("iot/ams"); })
                .Build();

            var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            Console.WriteLine("MQTT client subscribed to topic.");

            // The response contains additional data sent by the server after subscribing.
            response.DumpToConsole();
        }
    }

    public static Task Managed_Client()
    {
        // Creates a new client
        MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
            .WithClientId("Dev.To")
            .WithTcpServer("localhost", 707);

// Create client options objects
        ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
            .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
            .WithClientOptions(builder.Build())
            .Build();

// Creates the client object
        IManagedMqttClient _mqttClient = new MqttFactory().CreateManagedMqttClient();

        // Set up handlers
        // _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
        // _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
        // _mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);

        // Starts a connection with the Broker
        _mqttClient.StartAsync(options).GetAwaiter().GetResult();

    // Send a new message to the broker every second
        // while (true)
        // {
        //     string json = JsonConvert.SerializeObject(new { message = "Heyo :)", sent= DateTimeOffset.UtcNow });
        //     _mqttClient.PublishAsync("dev.to/topic/json", json);
        //
        //     Task.Delay(1000).GetAwaiter().GetResult();
        // }

        return Task.CompletedTask;
    }
    
    public static void OnConnected(MqttClientConnectedEventArgs obj)
    {
        Console.WriteLine("Successfully connected.");
    }

    public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
    {
        Console.WriteLine("Couldn't connect to broker.");
    }

    public static void OnDisconnected(MqttClientDisconnectedEventArgs obj)
    {
        Console.WriteLine("Successfully disconnected.");
    }
}