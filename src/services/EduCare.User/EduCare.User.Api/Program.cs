using EduCare.User.Api.BackgroundServices.UserUpload;
using EduCare.User.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEduCareUserDatabaseContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddControllers();
builder.Services.AddHostedService<UserUploadQueuedBackgroundService>();
builder.Services.AddSingleton<IUserUploadTaskQueue, UserUploadTaskQueue>();
var app = builder.Build();

app.MapDefaultControllerRoute();
app.Run();
