using Dapper;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class StoreRepository : GenericRepository<Store>
{
    public StoreRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Store>> GetAllByCityId(int cityId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Store>($"select * from Stores where CityId=@CityId", new { CityId = cityId }) as IList<Store>;
    }
}