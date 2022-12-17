using StoreManagementSystem.ViewModels.Category;
using StoreManagementSystem.ViewModels.Inventory;
using StoreManagementSystem.ViewModels.Photo;
using StoreManagementSystem.ViewModels.Purchase;
using StoreManagementSystem.ViewModels.Sale;

namespace StoreManagementSystem.ViewModels.Product;

public class ProductViewModel
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public float Price { get; set; }

    public CategoryViewModel Category { get; set; } = null!;
    
    public IList<PhotoViewModel> Photos { get; set; } = null!;
    
    public IList<PurchaseViewModel> Purchases { get; set; } = null!;
    
    public IList<SaleViewModel> Sales { get; set; } = null!;
    
    public IList<InventoryViewModel> Inventories { get; set; } = null!;
}