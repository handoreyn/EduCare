using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Abstract.Repository;

public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : Entity.Entity
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> DataSet;

    public AbstractRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        DataSet = _dbContext.Set<TEntity>();
    }

    public virtual async Task Add(TEntity entity)
    {
        await DataSet.AddAsync(entity);
    }

    public virtual async Task AddRange(IEnumerable<TEntity> entities)
    {
        await DataSet.AddRangeAsync(entities);
    }

    public virtual async Task Delete(int id)
    {
        var entity = await DataSet.FirstOrDefaultAsync(l => l.Id == id);
        DataSet.Remove(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        DataSet.Remove(entity);
    }

    public virtual void DeleteAll(IEnumerable<TEntity> entities)
    {
        DataSet.RemoveRange(entities);
    }

    public virtual Task<TEntity> Find(int id)
    {
        return DataSet.FirstOrDefaultAsync(l => l.Id == id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(int skip = 0, int itemCount = 10)
    {
        return await DataSet
            .OrderBy(u => u.Id)
            .Skip(skip)
            .Take(itemCount)
            .ToListAsync();
    }

    public virtual void Update(TEntity entity)
    {
        DataSet.Update(entity);
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

    public virtual Task<bool> IsExist(string data)
    {
        throw new NotImplementedException();
    }
}