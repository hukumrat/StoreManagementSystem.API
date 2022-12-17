namespace StoreManagementSystem.Models;

public class Wallet
{
    public int Id { get; set; }
    
    public int StoreId { get; set; }
    
    public float Income { get; set; }
    
    public float OutGoing { get; set; }
    
    public float Balance { get; set; }
}