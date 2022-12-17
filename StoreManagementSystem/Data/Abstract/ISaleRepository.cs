using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface ISaleRepository
{
    Task<IList<Wallet>> GetAllByProductIdAsync(int productId);
}