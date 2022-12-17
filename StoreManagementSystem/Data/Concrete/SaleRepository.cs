using Dapper;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class SaleRepository : GenericRepository<Sale>
{
    public SaleRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Wallet>> GetAllByProductIdAsync(int productId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Wallet>($"select * from Wallets where ProductId=@ProductId", new { ProductId = productId }) as IList<Wallet>;
    }
}