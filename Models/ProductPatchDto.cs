namespace SampleAPI.Models;

public class ProductPatchDto
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public ProductCategory? Category { get; set; }
}


