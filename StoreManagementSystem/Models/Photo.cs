namespace StoreManagementSystem.Models;

public class Photo
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Path { get; set; } = null!;
}