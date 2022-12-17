using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface IWalletRepository
{
    Task<IList<Wallet>> GetAllByStoreId(int storeId);

    Task<Wallet> GetByStoreIdAsync(int storeId);
}