namespace StoreManagementSystem.ViewModels.Product;

public class ProductPhotosViewModel
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public float Price { get; set; }
    public string Path { get; set; } = null!;
}