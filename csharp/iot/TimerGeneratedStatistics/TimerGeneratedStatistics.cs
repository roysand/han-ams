using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TimerGeneratedStatistics;

public class TimerGeneratedStatistics
{
    private readonly ILogger<TimerGeneratedStatistics> _logger;
    private readonly IConfiguration _configuration;
    private readonly IStatRepository<Detail> _statRepository;

    public TimerGeneratedStatistics(ILogger<TimerGeneratedStatistics> logger,
            IConfiguration configuration,
            IStatRepository<Detail> statRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _statRepository = statRepository;
    }
    [FunctionName("TimerGeneratedStatistics")]
    public async Task RunAsync([TimerTrigger("1 */1 * * * *")] TimerInfo myTimer, ILogger log,
        CancellationToken cancellationToken)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
        var result = await _statRepository.DailyTotal(DateTime.Now, cancellationToken);
        log.LogInformation("Finished!");
    }
}