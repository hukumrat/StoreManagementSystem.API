using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface ICategoryRepository
{
    Task<IList<Category>> GetAllByStoreId(int storeId);

    Task<Category> GetByStoreIdAsync(int storeId);
    
    Task<Category> GetByInventoryId(int inventoryId);
}