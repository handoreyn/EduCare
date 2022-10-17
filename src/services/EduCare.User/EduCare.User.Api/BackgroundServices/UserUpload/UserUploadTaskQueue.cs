using System.Threading.Channels;

namespace EduCare.User.Api.BackgroundServices.UserUpload;

public class UserUploadTaskQueue : IUserUploadTaskQueue
{
    private readonly Channel<Func<CancellationToken, Task>> _queue;
    private readonly ILogger<UserUploadTaskQueue> _logger;


    public UserUploadTaskQueue(ILogger<UserUploadTaskQueue> logger)
    {
        _logger = logger;

        var options = new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<Func<CancellationToken, Task>>(options);
    }
    public async Task<Func<CancellationToken, Task>> DeQueueWorkItem(CancellationToken cancellationToken)
    {
        var workItem = await _queue.Reader.ReadAsync(cancellationToken);
        _logger.LogInformation($"Task DeQueued at: {DateTime.UtcNow}");
        return workItem;
    }

    public async Task QueueWorkItem(Func<CancellationToken, Task> workItem)
    {
        if (workItem == null)
        {
            _logger.LogInformation("Work Item was null.");
            return;
        }

        await _queue.Writer.WriteAsync(workItem);
    }
}