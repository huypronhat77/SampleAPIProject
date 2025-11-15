namespace SampleAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum ProductCategory
{
    Electronics,
    Clothing,
    Food,
    Books,
    Home,
    Sports,
    Toys,
    Beauty,
    Automotive,
    Other
}


