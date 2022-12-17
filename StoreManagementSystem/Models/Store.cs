namespace StoreManagementSystem.Models;

public class Store
{
    public int Id { get; set; }
    
    public int CityId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public string Phone { get; set; } = null!;
}