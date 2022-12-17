using StoreManagementSystem.Models;

namespace StoreManagementSystem.Data.Abstract;

public interface IPhotoRepository
{
    Task<IList<Photo>> GetAllByProductId(int productId);
}