using StoreManagementSystem.ViewModels.Product;

namespace StoreManagementSystem.ViewModels.Purchase;

public class PurchaseViewModel
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
    
    public float Price { get; set; }

    public ProductViewModel Product { get; set; } = null!;
}