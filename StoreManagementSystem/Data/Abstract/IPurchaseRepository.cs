using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface IPurchaseRepository
{
    Task<IList<Purchase>> GetAllByProductId(int productId);
}