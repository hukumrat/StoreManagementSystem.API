using StoreManagementSystem.ViewModels.Product;

namespace StoreManagementSystem.ViewModels.Sale;

public class SaleViewModel
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public ProductViewModel Product { get; set; } = null!;
}