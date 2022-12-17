using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class CityRepository : GenericRepository<City>, ICityRepository
{
    public CityRepository(string tableName) : base(tableName)
    {
    }
}