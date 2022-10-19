using EduCare.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EduCare.User.Api.Extensions;

public static class DatabaseContextExtension
{
    public static void AddEduCareUserDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration["EduCareUserDatabaseConnectionString"];
        Console.WriteLine(connStr);
        services.AddDbContext<EduCareDatabaseContext>(options => options
            .UseSqlServer(connStr, b =>
            b.MigrationsAssembly("EduCare.User.Api")
            ));
    }
}