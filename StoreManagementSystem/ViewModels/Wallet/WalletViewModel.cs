using StoreManagementSystem.ViewModels.Store;

namespace StoreManagementSystem.ViewModels.Wallet;

public class WalletViewModel
{
    public int Id { get; set; }
    
    public int StoreId { get; set; }
    
    public float Income { get; set; }
    
    public float OutGoing { get; set; }
    
    public float Balance { get; set; }

    public StoreViewModel Store { get; set; } = null!;
}