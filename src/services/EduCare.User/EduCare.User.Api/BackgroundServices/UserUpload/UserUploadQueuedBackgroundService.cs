namespace EduCare.User.Api.BackgroundServices.UserUpload;

public class UserUploadQueuedBackgroundService : BackgroundService
{
    private readonly ILogger<UserUploadQueuedBackgroundService> _logger;

    public UserUploadQueuedBackgroundService(ILogger<UserUploadQueuedBackgroundService> logger, IUserUploadTaskQueue taskQueue)
    {
        _logger = logger;
        _taskQueue = taskQueue;
    }

    private IUserUploadTaskQueue _taskQueue { get; } 


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _taskQueue.DeQueueWorkItem(stoppingToken);
            try
            {
                await workItem(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during User file processing.");
            }
        }
    }
}