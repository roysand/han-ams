using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using Domain.Entities;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBusReadFunction
{
    public class ServiceBusReadFunction
    {
        private readonly IConfiguration _configuration;
        private readonly IAppclicationDbContext _appclicationDbContext;

        public ServiceBusReadFunction(IConfiguration configuration
            ,IAppclicationDbContext appclicationDbContext)
        {
            _configuration = configuration;
            _appclicationDbContext = appclicationDbContext;
        }
        
        [FunctionName("ServiceBusReadFunction")]
        public async Task Run([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var topic = _configuration.GetValue<string>("ApplicationSettings:topic");
            var subscription = _configuration.GetValue<string>("ApplicationSettings:subscription");

            log.LogInformation($"Read from ServiceBus with Topic='{topic}' and Subscription='{subscription}'");
            
            var messageCount = await GetCounterMessages();
            log.LogInformation($"Elements in queue: {messageCount}");

            var data = (from m in _appclicationDbContext.MinuteSet
                where m.TimeStamp == new DateTime(2022,5,20,14,50,0)
                select m);
            
            log.LogInformation($"Number of records found: {data.Count()}");
            
            if (messageCount > 0)
            {
                var client = new ServiceBusClient(_configuration.GetConnectionString("QueueConnection"));
                var receiver = client.CreateReceiver(topic, subscription);
            
                IReadOnlyList<ServiceBusReceivedMessage> receivedMessages =
                    await receiver.ReceiveMessagesAsync(maxMessages: Convert.ToInt32(messageCount));
                
                foreach (var messages in receivedMessages)
                {
                    string body = messages.Body.ToString();
                    log.LogInformation($"Body='{body}' - {messages.EnqueuedTime}");
                    await receiver.CompleteMessageAsync(messages);
                }
            
                await receiver.DisposeAsync();
                await client.DisposeAsync();
            }
        }
        
        public async Task<long> GetCounterMessages()
        {
            var client = new ManagementClient(_configuration.GetConnectionString("QueueConnection"));    
            var subs = await client.GetSubscriptionRuntimeInfoAsync("bas", "test");
            var countForThisSubscription = subs.MessageCount;  //// (Comes back as a Long.)               
            return countForThisSubscription;
        }
    }
}
