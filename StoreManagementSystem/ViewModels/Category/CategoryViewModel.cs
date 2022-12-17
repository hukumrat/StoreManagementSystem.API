using StoreManagementSystem.ViewModels.Product;
using StoreManagementSystem.ViewModels.Store;

namespace StoreManagementSystem.ViewModels.Category;

public class CategoryViewModel
{
    public int Id { get; set; }

    public int StoreId { get; set; }

    public string Name { get; set; } = null!;

    public StoreViewModel Store { get; set; } = null!;

    public IList<ProductViewModel> Products { get; set; } = null!;
}