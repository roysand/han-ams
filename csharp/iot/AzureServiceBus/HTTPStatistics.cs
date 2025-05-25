using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureServiceBus
{
    public class HTTPStatistics
    {
        private readonly ILogger<HTTPStatistics> _log;
        private readonly IConfiguration _configuration;
        private readonly IStatRepository<Detail> _statRepository;

        public HTTPStatistics(ILogger<HTTPStatistics> log,
            IConfiguration configuration,
            IStatRepository<Detail> statRepository)
        {
            _log = log;
            _configuration = configuration;
            _statRepository = statRepository;
        }

        [Function("stat")]
        public async Task<DailyTotalVm> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext)
        {
            var log = executionContext.GetLogger<HTTPStatistics>();
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Create a cancellation token
            var cancellationToken = new CancellationToken();

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var result = await _statRepository.DailyTotal(DateTime.Now, cancellationToken);
            
            // Create the response
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);
            
            return result;
        }
    }
}
