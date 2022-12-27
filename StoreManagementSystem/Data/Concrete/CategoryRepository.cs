using Dapper;
using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Models;
using StoreManagementSystem.ViewModels.Category;

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
    
    public async Task<IList<CategoryWithStoreAndCityViewModel>> GetAllWithStoreAndCity()
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<CategoryWithStoreAndCityViewModel>(
                "select Categories.Id as CategoryId, " +
                "Categories.Name as CategoryName, " +
                "Stores.Name as StoreName, " +
                "Stores.Address as StoreAddress, " +
                "Stores.Description as StoreDescription, " +
                "Stores.Phone as StorePhone, " +
                "Cities.Name as CityName from Categories " +
                "full outer join Stores " +
                "on Categories.StoreId = Stores.Id " +
                "full outer join Cities " +
                "on Stores.CityId = Cities.Id " +
                "where Stores.CityId = " +
                "any (select Id from Cities)")
            as IList<CategoryWithStoreAndCityViewModel>;
    }
}