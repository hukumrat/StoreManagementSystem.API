using System.Collections;
using Dapper;
using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Product>> GetAllByCategoryId(int categoryId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Product>($"select * from Products where CategoryId=@CategoryId", new { CategoryId = categoryId }) as IList<Product>;
    }
}