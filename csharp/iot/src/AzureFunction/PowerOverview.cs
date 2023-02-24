using System.Collections.Generic;
using System.Net;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace AzureFunction;

public class PowerOverview
{
    private readonly IStatRepository<Detail> _statRepository;

    public PowerOverview(IStatRepository<Detail> statRepository)
    {
        _statRepository = statRepository;
    }
    [Function("PowerOverview")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("PowerOverview");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        var result = await _statRepository.DailyTotal(DateTime.Now,new CancellationToken());
        return new OkObjectResult(result);
    }
}