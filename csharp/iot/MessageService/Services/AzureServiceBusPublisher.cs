using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MessageService.Contracts;
using Newtonsoft.Json;

namespace MessageService.Services
{ 
    public class AzureServiceBusPublisher : IMessagePublisher
    {
        // TODO: Remove connectionstrin from code!! IMPORTANT
        private string ConnectionString =
            "Endpoint=sb://sandaas-iot.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=r3s1U4/lzrCM2kF8BaLOIinQVWbwnPUlYhd91PjRYdQ=";
        private string TopicName = "ams";
        private string SubscriptionFilter = "raw";
        
        private readonly ServiceBusSender _sender;
        private readonly ServiceBusClient _client;

        public AzureServiceBusPublisher(ServiceBusClient serviceBusClient)
        {
            _client = serviceBusClient;
            _sender = serviceBusClient.CreateSender(TopicName);
        }

        ~AzureServiceBusPublisher()
        {
            _sender.DisposeAsync();
            _client.DisposeAsync();
        }

        public AzureServiceBusPublisher()
        {
            _client = new ServiceBusClient(ConnectionString);
            _sender = _client.CreateSender(TopicName);
        }

        public async Task Publish<T>(T obj)
        {
            try
            {
                var objAsText = JsonConvert.SerializeObject(obj);
                var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(objAsText));
                message.To = SubscriptionFilter;
                await _sender.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                // TODO: Send to Error Queue
                Console.WriteLine(ex);
            }
        }

        public async Task Publish(string raw)
        {
            try
            {
                var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(raw));
                await _sender.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                // TODO: Send to Error Queue
                Console.WriteLine(ex);
            }
        }
    }
}