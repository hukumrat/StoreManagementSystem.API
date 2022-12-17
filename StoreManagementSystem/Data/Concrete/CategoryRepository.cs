using Dapper;
using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class CategoryRepository : GenericRepository<Category>
{
    
    
    public CategoryRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Category>> GetAllByStoreId(int storeId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Category>($"select * from Categories where StoreId=@StoreId", new { StoreId = storeId }) as IList<Category>;
    }

    public async Task<Category> GetByStoreIdAsync(int storeId)
    {
        using var connection = CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Category>($"select * from Categories where StoreId=@StoreId", new { StoreId = storeId });
    }
}