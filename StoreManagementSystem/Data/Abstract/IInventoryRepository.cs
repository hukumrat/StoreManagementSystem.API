using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface IInventoryRepository
{
    Task<IList<Inventory>> GetAllByStoreId(int storeId);

    Task<Inventory> GetByStoreIdAsync(int storeId);

    Task<IList<Inventory>> GetAllByProductId(int productId);
}