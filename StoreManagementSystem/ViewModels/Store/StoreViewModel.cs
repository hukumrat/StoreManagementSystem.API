using System.Collections;
using StoreManagementSystem.ViewModels.Category;
using StoreManagementSystem.ViewModels.City;
using StoreManagementSystem.ViewModels.Inventory;
using StoreManagementSystem.ViewModels.Wallet;

namespace StoreManagementSystem.ViewModels.Store;

public class StoreViewModel
{
    public int Id { get; set; }
    
    public int CityId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public string Phone { get; set; } = null!;
    
    public CityViewModel City { get; set; } = null!;
    
    public IList<CategoryViewModel> Categories { get; set; } = null!;
    
    public IList<WalletViewModel> Wallets { get; set; } = null!;
    
    public IList<InventoryViewModel> Inventories { get; set; } = null!;
}