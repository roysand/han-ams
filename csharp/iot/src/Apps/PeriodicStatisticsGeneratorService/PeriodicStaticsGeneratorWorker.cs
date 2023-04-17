using Application.Common.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace PeriodicStatisticsGeneratorService;

public class PeriodicStaticsGeneratorWorker : BackgroundService
{
    private readonly ILogger<PeriodicStaticsGeneratorWorker> _logger;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IStatRepository<Detail> _statRepository;
    private readonly IDateTime _dateTime;
    private readonly TimeSpan _period = TimeSpan.FromMilliseconds(2500);
    private DateTime _lastRunTimeMinute;
    private DateTime _lastRunTimeHour;
    public bool IsEnabled { get; set; }

    public PeriodicStaticsGeneratorWorker(ILogger<PeriodicStaticsGeneratorWorker> logger
        ,IHostApplicationLifetime hostApplicationLifetime
        ,IStatRepository<Domain.Entities.Detail> statRepository
        ,IDateTime dateTime)
    {
        _logger = logger;
        _hostApplicationLifetime = hostApplicationLifetime;
        _statRepository = statRepository;
        _dateTime = dateTime;
        _lastRunTimeMinute = _dateTime.Now;
        _lastRunTimeHour = _dateTime.Now;
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
                    now = _dateTime.Now;
                    
                    _logger.LogInformation("{Now}- Timer is running! (min) {LastRunMinute} (hour) {LastRunHour}", now,
                        _lastRunTimeMinute, _lastRunTimeHour);
                    
                    if (Math.Abs((now.Hour*100 + now.Minute) - (_lastRunTimeMinute.Hour * 100 + _lastRunTimeMinute.Minute)) >= 1 && now.Second >= 2)
                    {
                        _logger.LogInformation("Creating statistics for minute");
                        _lastRunTimeMinute = now;
                        await _statRepository.GenerateMinutePowerUsageStatistics(stoppingToken);
                    }

                    if (Math.Abs((now.Hour - _lastRunTimeHour.Hour)) >= 1 && now.Minute >= 2)
                    {
                        _logger.LogInformation("Creating statistics for hour");
                        _lastRunTimeHour = now;
                        await _statRepository.GenerateHourPowerUsageStatistics(stoppingToken);
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
