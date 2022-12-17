namespace StoreManagementSystem.Models;

public class Purchase
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
    
    public float Price { get; set; }
}