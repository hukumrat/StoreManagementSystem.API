using StoreManagementSystem.ViewModels.Store;

namespace StoreManagementSystem.ViewModels.City;

public class CityViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public IList<StoreViewModel> Stores { get; set; } = null!;
}