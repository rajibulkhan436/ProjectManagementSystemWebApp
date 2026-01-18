using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Services.Contracts;

namespace ProjectManagementSystem.Services.Services.Background
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IBackgroundTaskQueue _queue;
        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IBackgroundTaskQueue queue)
        {
            _logger = logger;
            _queue = queue;       
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Services Started.");

            while (!stoppingToken.IsCancellationRequested) 
            {
                var workItem = await _queue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error occurred while processing background task.");
                }
            }
            
        }
    }
}
