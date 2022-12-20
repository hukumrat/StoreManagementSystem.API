using System.Collections;
using Dapper;
using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Models;
using StoreManagementSystem.ViewModels.Product;

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

    public async Task<IList<ProductPhotosViewModel>> GetAllProductsWithPhotos()
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<ProductPhotosViewModel>(
                "select ProductId, CategoryId, Name, Description, Price, Path from Photos full outer join Products on Photos.ProductId = Products.Id")
            as IList<ProductPhotosViewModel>;
    }
}