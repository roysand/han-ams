namespace GenerateStatistics.Console;

public class GenerateStatisticsWorker : BackgroundService
{
    private readonly ILogger<GenerateStatisticsWorker> _logger;
    public bool IsEnabled { get; set; }
    
    public GenerateStatisticsWorker(ILogger<GenerateStatisticsWorker> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Hello from worker");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (IsEnabled)
                {
                    _logger.LogInformation("Worker is enabled!");
                }
                else
                {
                    _logger.LogInformation("Worker is NOT enabled!");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to execute GenerateStatisticWorker - exception message: '{e.Message}'");
            }
            finally
            {
                CancellationTokenSource cancellationToken = new CancellationTokenSource();
                stoppingToken = cancellationToken.Token;
                cancellationToken.Cancel();
            }
        }
    }
}