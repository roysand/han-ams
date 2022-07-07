// See https://aka.ms/new-console-template for more information

using Infrastructure.Clients;

Console.WriteLine("Hello, World!");

await TestMqttBroker.ClientSubscribeSamples.Subscribe_Topic();

var client = new MqttManagedClient();
await client.SubscribeAsync("iot/ams", 1);

Console.ReadKey();
await client.Disconnect();


