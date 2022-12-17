namespace StoreManagementSystem.ViewModels.Product;

public record ProductUpdateViewModel(int CategoryId,
    string Name,
    string Description,
    float Price);