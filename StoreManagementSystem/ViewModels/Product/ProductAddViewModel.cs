namespace StoreManagementSystem.ViewModels.Product;

public record ProductAddViewModel(int CategoryId,
    string Name,
    string Description,
    float Price);