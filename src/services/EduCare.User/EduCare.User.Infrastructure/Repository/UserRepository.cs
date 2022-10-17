using BuildingBlocks.Abstract.Repository;
using EduCare.User.Core.Abstract.Repository;
using EduCare.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EduCare.User.Infrastructure.Repository;
public class UserRepository : AbstractRepository<User.Core.Entities.User>, IUserRepository
{
    public UserRepository(EduCareDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task BulkInsertUsers(IEnumerable<Core.Entities.User> users)
    {
        await DataSet.AddRangeAsync(users);
    }

    public async Task<User.Core.Entities.User> FindByEmail(string email)
    {
        var user = await DataSet.FirstOrDefaultAsync(l => l.Email.Equals(email));
        return user;
    }

    public override async Task<IEnumerable<Core.Entities.User>> GetAll(int skip = 0, int itemCount = 10)
    {
        return await DataSet.OrderBy(l => l.CreateDate)
            .Skip(skip)
            .Take(itemCount)
            .ToListAsync();
    }

    public override async Task<bool> IsExist(string data)
    {
        return await DataSet
            .Where(d => d.Email.Equals(data))
            .Select(t => true)
            .FirstOrDefaultAsync();
    }
}