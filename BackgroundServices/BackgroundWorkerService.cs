public class BackgroundWorkerService : BackgroundService //IHostedService
{
    private readonly ILogger<BackgroundWorkerService> _logger;
    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start async");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Executing...");
            await Task.Delay(1000);
        }
    }
    //This will block the main thread, API - controller is not working. So inheriting from background service
    //public async Task StartAsync(CancellationToken cancellationToken)
    //{
    //    _logger.LogInformation("Start async");
    //    while(!cancellationToken.IsCancellationRequested)
    //    {
    //        _logger.LogInformation("Executing...");
    //        await Task.Delay(1000);
    //    }
    //}

    //public Task StopAsync(CancellationToken cancellationToken)
    //{
    //    _logger.LogInformation("Stop async");
    //    return Task.CompletedTask;
    //}
}