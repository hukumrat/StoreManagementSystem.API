namespace StoreManagementSystem.ViewModels.Inventory;

public record InventoryUpdateViewModel(int ProductId,
    int StoreId,
    int Quantity);