namespace StoreManagementSystem.ViewModels.Category;

public class CategoryWithStoreAndCityViewModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string StoreName { get; set; } = null!;
    public string StoreAddress { get; set; } = null!;
    public string StoreDescription { get; set; } = null!;
    public string StorePhone { get; set; } = null!;
    public string CityName { get; set; } = null!;
}