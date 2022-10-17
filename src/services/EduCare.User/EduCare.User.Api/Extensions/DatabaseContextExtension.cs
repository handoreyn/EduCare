using EduCare.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EduCare.User.Api.Extensions;

public static class DatabaseContextExtension
{
    public static void AddEduCareUserDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EduCareDatabaseContext>(options => options
            .UseSqlServer(configuration["EduCareUserDatabaseConnectionString"], b =>
            b.MigrationsAssembly("EduCare.User.Api")
            ));
    }
}