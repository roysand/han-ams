using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
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

        [FunctionName("stat")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, CancellationToken cancellationToken)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var result = await _statRepository.DailyTotal(DateTime.Now, cancellationToken);
            return new OkObjectResult(result);
            
            return new OkObjectResult(responseMessage);
        }
    }
}
