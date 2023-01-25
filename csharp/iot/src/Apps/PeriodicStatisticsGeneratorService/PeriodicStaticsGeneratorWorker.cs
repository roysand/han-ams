using Application.Common.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace PeriodicStatisticsGeneratorService;

public class PeriodicStaticsGeneratorWorker : BackgroundService
{
    private readonly ILogger<PeriodicStaticsGeneratorWorker> _logger;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IStatRepository<Detail> _statRepository;
    private readonly TimeSpan _period = TimeSpan.FromMilliseconds(2500);
    private DateTime lastRunTime;
    public bool IsEnabled { get; set; }

    public PeriodicStaticsGeneratorWorker(ILogger<PeriodicStaticsGeneratorWorker> logger
        ,IHostApplicationLifetime hostApplicationLifetime
        ,IStatRepository<Domain.Entities.Detail> statRepository)
    {
        _logger = logger;
        _hostApplicationLifetime = hostApplicationLifetime;
        _statRepository = statRepository;
        lastRunTime = DateTime.Now;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        IsEnabled = true;
        DateTime now;

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (IsEnabled)
                {
                    now = DateTime.Now;
                    
                    _logger.LogInformation($"{now}- Is something happening now");
                    
                    if (now.Minute - lastRunTime.Minute >= 1)
                    {
                        _logger.LogInformation("Creating statistics");
                        lastRunTime = now;
                        var overviewData = await _statRepository.DailyTotal(DateTime.Now, stoppingToken);
                        if (overviewData != null)
                        {
                            _logger.LogInformation(JsonSerializer.Serialize(overviewData));
                        }
                    }
                }
                else
                {
                    _logger.LogInformation("Worker is NOT enabled!, stopping");
                    timer.Dispose();
                    _hostApplicationLifetime.StopApplication();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"Failed to execute PeriodicStaticsGeneratorWorker - exception message: '{e.Message}'");
            }
        }
    }
}
