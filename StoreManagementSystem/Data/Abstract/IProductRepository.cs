using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface IProductRepository
{
    Task<IList<Product>> GetAllByCategoryId(int categoryId);

    Task<Product> GetByInventoryId(int inventoryId);

    Task<Photo> GetByPhotoId(int photoId);

    Task<Photo> GetByPurchaseId(int purchaseId);

    Task<Photo> GetBySaleId(int saleId);
}