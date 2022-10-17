namespace BuildingBlocks.Abstract.Repository;

public interface IRepository<T>
{
    Task<bool> IsExist(string email);
    Task<T> Find(int id);
    Task<IEnumerable<T>> GetAll(int skip = 0, int itemCount = 10);
    Task Add(T entity);
    Task AddRange(IEnumerable<T> entities);
    void Update(T entity);
    Task Delete(int id);
    void Delete(T entity);
    void DeleteAll(IEnumerable<T> entities);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}