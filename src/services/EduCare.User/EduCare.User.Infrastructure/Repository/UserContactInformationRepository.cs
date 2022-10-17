using BuildingBlocks.Abstract.Repository;
using EduCare.User.Core.Abstract.Repository;
using EduCare.User.Core.Entities;
using EduCare.User.Infrastructure.Database;

namespace EduCare.User.Infrastructure.Repository;

public class UserContactInformationRepository : AbstractRepository<UserContactInformation>, IUserContactInformationRepository
{
    public UserContactInformationRepository(EduCareDatabaseContext dbContext) : base(dbContext)
    {
    }
}