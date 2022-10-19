using BuildingBlocks.Enums;
using EduCare.User.Core.Abstract.Repository;
using EduCare.User.Core.Enums;
using Microsoft.VisualBasic.FileIO;

namespace EduCare.User.Api.BackgroundServices.UserUpload;

public class UserUploadTask
{
    private readonly IServiceScopeFactory _serviceFactory;
    private readonly string _filePath;
    public UserUploadTask(string filePath, IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
        _filePath = filePath;
    }

    public async Task Process(CancellationToken token)
    {
        var scope = _serviceFactory.CreateScope();
        var logger = scope.ServiceProvider.GetService<ILogger<UserUploadTask>>();
        logger.LogInformation($"User upload task has been started at: {DateTime.UtcNow}");

        var users = new Dictionary<string, User.Core.Entities.User>();
        using (var textParser = new TextFieldParser(_filePath))
        {
            textParser.TextFieldType = FieldType.Delimited;
            textParser.SetDelimiters(",");

            while (!textParser.EndOfData)
            {
                var fields = textParser.ReadFields();

                if (users.ContainsKey(fields[1])) continue;

                var user = new User.Core.Entities.User
                {
                    Email = fields[1],
                    Password = fields[2],
                    FullName = fields[3],
                    BirthDate = DateTime.Parse(fields[4]),
                    CreateDate = DateTime.Parse(fields[5]),
                    UpdateDate = DateTime.Parse(fields[6]),
                    UserType = (UserType)int.Parse(fields[7]),
                    Status = StatusEnumType.active
                };
                users.TryAdd(user.Email, user);
            }
        }

        var repository = scope.ServiceProvider.GetService<IUserRepository>();
        await repository.BulkInsertUsers(users.Values);
        var result = await repository.SaveChangesAsync();
        logger.LogInformation($"User upload task has been completed at: {DateTime.UtcNow}");
    }
}