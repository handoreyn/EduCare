namespace EduCare.User.Api.BackgroundServices.UserUpload;

public interface IUserUploadTaskQueue
{
    Task QueueWorkItem(Func<CancellationToken, Task> workItem);
    Task<Func<CancellationToken, Task>> DeQueueWorkItem(CancellationToken cancellationToken);
}