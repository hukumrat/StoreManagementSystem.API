namespace StoreManagementSystem.Data.Abstract;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task DeleteRowAsync(int id);
    Task<T> GetAsync(int id);
    Task<int> SaveRangeAsync(IEnumerable<T> list);
    
    Task<T> UpdateAsync(int id, T entity);
    Task<T> InsertAsync(T entity);
}