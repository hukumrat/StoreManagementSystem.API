using Dapper;
using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class PurchaseRepository : GenericRepository<Purchase>
{
    public PurchaseRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Purchase>> GetAllByProductIdAsync(int productId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Purchase>($"select * from Purchases where ProductId=@ProductId", new { ProductId = productId }) as IList<Purchase>;
    }
}