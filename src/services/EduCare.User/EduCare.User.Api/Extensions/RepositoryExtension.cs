using BuildingBlocks.Abstract.Repository;
using EduCare.User.Core.Abstract.Repository;
using EduCare.User.Infrastructure.Repository;

namespace EduCare.User.Api.Extensions;

public static class RepositoryExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IUserContactInformationRepository, UserContactInformationRepository>();
    }
}