using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface IStoreRepository
{
    Task<IList<Store>> GetAllByCityId(int cityId);

    Task<Store> GetByWalletId(int walletId);

    Task<Store> GetByInventoryId(int inventoryId);
}