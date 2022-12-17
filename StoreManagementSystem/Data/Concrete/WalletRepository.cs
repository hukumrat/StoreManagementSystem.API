using Dapper;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class WalletRepository : GenericRepository<Wallet>
{
    public WalletRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Wallet>> GetAllByStoreId(int storeId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Wallet>($"select * from Wallets where StoreId=@StoreId", new { StoreId = storeId }) as IList<Wallet>;
    }

    public async Task<Wallet> GetByStoreIdAsync(int storeId)
    {
        using var connection = CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Wallet>($"select * from Wallets where StoreId=@StoreId", new { StoreId = storeId });
    }
}