using Dapper;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Concrete;

public class PhotoRepository : GenericRepository<Photo>
{
    public PhotoRepository(string tableName) : base(tableName)
    {
    }

    public async Task<IList<Photo>> GetAllByProductIdAsync(int productId)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Photo>($"select * from Photos where ProductId=@ProductId", new { ProductId = productId }) as IList<Photo>;
    }
}