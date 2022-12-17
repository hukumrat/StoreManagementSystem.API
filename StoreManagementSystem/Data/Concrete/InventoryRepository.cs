using System.Collections;
using Dapper;
using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class InventoryRepository : GenericRepository<Inventory>
{
    public InventoryRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Inventory>> GetAllByStoreId(int storeId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Inventory>($"select * from Inventories where StoreId=@StoreId", new { StoreId = storeId }) as IList<Inventory>;
    }

    public async Task<Inventory> GetByStoreIdAsync(int storeId)
    {
        using var connection = CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Inventory>($"select * from Inventories where StoreId=@StoreId", new { StoreId = storeId });
    }

    public async Task<IList<Inventory>> GetAllByProductIdAsync(int productId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Inventory>($"select * from Inventories where ProductId=@ProductId", new { ProductId = productId }) as IList<Inventory>;
    }
}