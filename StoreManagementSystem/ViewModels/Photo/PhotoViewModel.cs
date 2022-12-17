using StoreManagementSystem.ViewModels.Product;

namespace StoreManagementSystem.ViewModels.Photo;

public class PhotoViewModel
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Path { get; set; } = null!;

    public ProductViewModel Product { get; set; } = null!;
}