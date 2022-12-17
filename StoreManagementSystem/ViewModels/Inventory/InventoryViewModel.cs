using StoreManagementSystem.ViewModels.Product;
using StoreManagementSystem.ViewModels.Store;

namespace StoreManagementSystem.ViewModels.Inventory;

public class InventoryViewModel
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    
    public int StoreId { get; set; }

    public int Quantity { get; set; }

    public StoreViewModel Store { get; set; } = null!;

    public ProductViewModel Product { get; set; } = null!;
}