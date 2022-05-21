using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using Domain.Entities;
using MBusReader.Contracts;
using MessageParser.Code;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBusReadFunction
{
    public class ServiceBusReadFunction
    {
        private readonly IConfiguration _configuration;
        private readonly IRawRepository<RawData> _rawRepository;
        private readonly IDetailRepository<Detail> _detailRepository;

        public ServiceBusReadFunction(IConfiguration configuration
            ,IRawRepository<RawData> rawRepository
            ,IDetailRepository<Detail> detailRepository)
        {
            _configuration = configuration;
            _rawRepository = rawRepository;
            _detailRepository = detailRepository;
        }
        
        [FunctionName("ServiceBusReadFunction")]
        public async Task Run([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, ILogger log
            ,CancellationToken cancellationToken)
        {
            var topic = _configuration.GetValue<string>("ApplicationSettings:topic");
            var subscription = _configuration.GetValue<string>("ApplicationSettings:subscription");
            var maxMessagesToRead = _configuration.GetValue<long>("ApplicationSettings:maxMessagesToRead");

            log.LogInformation($"Read from ServiceBus with Topic='{topic}' and Subscription='{subscription}'");
            
            var messageCount = await GetCounterMessages(topic, subscription);
            log.LogInformation($"Elements in queue: {messageCount} and max messages to read={maxMessagesToRead} ");
            messageCount = Math.Min(maxMessagesToRead, messageCount);

            if (messageCount > 0)
            {
                var client = new ServiceBusClient(_configuration.GetConnectionString("QueueConnection"));
                var receiver = client.CreateReceiver(topic, subscription);

                IReadOnlyList<ServiceBusReceivedMessage> receivedMessages =
                    await receiver.ReceiveMessagesAsync(maxMessages: Convert.ToInt32(messageCount));

                try
                {
                    foreach (var message in receivedMessages)
                    {
                        string body = message.Body.ToString();
                        var raw = JsonSerializer.Deserialize<RawData>(message.Body);
                        _rawRepository.Add(raw);
                        ParseMessageAndAddToDatabase(raw);

                        log.LogInformation($"Body='{body}' - {message.EnqueuedTime}");
                        await receiver.CompleteMessageAsync(message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    await _rawRepository.SaveChangesAsync(cancellationToken);
                    await _detailRepository.SaveChangesAsync(cancellationToken);
                }

                await receiver.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        private void ParseMessageAndAddToDatabase(RawData raw)
        {
            var hdlcMessage = ParseMessage((raw.Raw));
            
            foreach (var data in hdlcMessage.Data)
            {
                var detail = new Detail()
                {
                    MeasurementId = raw.MeasurementId,
                    TimeStamp = raw.TimeStamp,
                    Location = raw.Location,
                    ObisCode = data.ObisCode,
                    ObisCodeId = data.ObisCodeId,
                    Unit = data.Unit,
                    Name = data.Name,
                    ValueStr = data.Name,
                    ValueNum = data.Value
                };

                _detailRepository.Add(detail);

                Console.WriteLine(JsonSerializer.Serialize(detail));
            }
        }
        private async Task<long> GetCounterMessages(string topic, string subscription)
        {
            var client = new ManagementClient(_configuration.GetConnectionString("QueueConnection"));    
            var subs = await client.GetSubscriptionRuntimeInfoAsync(topic, subscription);
            var countForThisSubscription = subs.MessageCount;  //// (Comes back as a Long.)               
            return countForThisSubscription;
        }
        
        private IHDLCMessage ParseMessage(string messageAsRawString)
        {
            var parser = new Parser();
            messageAsRawString = messageAsRawString.Replace(" ", "");
            var messageByteArray = Enumerable.Range(0, messageAsRawString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(messageAsRawString.Substring(x, 2), 16).ToString("X2"));

            var message = messageByteArray.Skip(1).Take(messageByteArray.Count() - 1).ToList();
            message.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();
            var hdlcMessage = parser.Parse(messageByteArray.Select(x => (byte)(Convert.ToInt32(x, 16))).ToList());

            return hdlcMessage;
        }
    }
}
