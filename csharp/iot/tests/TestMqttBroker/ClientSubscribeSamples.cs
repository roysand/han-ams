
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;

namespace TestMqttBroker;

public static class ClientSubscribeSamples
{
    public static async Task Handle_Received_Application_Message()
    {
        /*
         * This sample subscribes to a topic and processes the received message.
         */

        var mqttFactory = new MqttFactory();

        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("iot-phil4787")
                .WithCredentials("iot","i3hYtte")
                .Build();

            // Setup message handling before connecting so that queued messages
            // are also handled properly. When there is no event handler attached all
            // received messages get lost.
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine("Received application message.");
                e.DumpToConsole();

                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic("iot/ams"); })
                .Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            Console.WriteLine("MQTT client subscribed to topic.");

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
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
                .WithTcpServer("iot-phil4787")
                .WithCredentials("iot","i3hYtte")
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

    public static async Task Managed_Client()
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