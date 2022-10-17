using BuildingBlocks.Abstract.Repository;

namespace EduCare.User.Core.Abstract.Repository;

public interface IUserRepository : IRepository<Entities.User>
{
    Task<User.Core.Entities.User> FindByEmail(string email);
    Task BulkInsertUsers(IEnumerable<User.Core.Entities.User> users);
}