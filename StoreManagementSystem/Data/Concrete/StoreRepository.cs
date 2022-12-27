using Dapper;
using StoreManagementSystem.Models;
using StoreManagementSystem.ViewModels.Store;

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
    
    public async Task<IList<StoreCityNamesViewModel>> GetAllStoresWithCityNames()
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<StoreCityNamesViewModel>(
                "select Stores.Id, Stores.Name, Address, Description, Phone, Cities.Name as City " +
                "from Stores " +
                "full outer join Cities " +
                "on Stores.CityId = Cities.Id " +
                "where Stores.CityId = " +
                "any (select Id from Cities)")
            as IList<StoreCityNamesViewModel>;
    }
}