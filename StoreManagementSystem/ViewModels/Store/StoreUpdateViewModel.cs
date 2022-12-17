namespace StoreManagementSystem.ViewModels.Store;

public record StoreUpdateViewModel(int CityId,
    string Name,
    string Address,
    string Description,
    string Phone);