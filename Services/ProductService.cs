using SampleAPI.Models;

namespace SampleAPI.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public ProductService()
    {
        SeedData();
    }

    private void SeedData()
    {
        var seedProducts = new List<Product>
        {
            new Product
            {
                Id = _nextId++,
                Name = "Laptop Dell XPS 15",
                Price = 1299.99m,
                Description = "High-performance laptop with 15-inch display",
                Category = ProductCategory.Electronics,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                UpdatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Wireless Mouse",
                Price = 29.99m,
                Description = "Ergonomic wireless mouse with USB receiver",
                Category = ProductCategory.Electronics,
                CreatedAt = DateTime.UtcNow.AddDays(-25),
                UpdatedAt = DateTime.UtcNow.AddDays(-25)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Cotton T-Shirt",
                Price = 19.99m,
                Description = "Comfortable cotton t-shirt, available in multiple colors",
                Category = ProductCategory.Clothing,
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                UpdatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Organic Coffee Beans",
                Price = 15.99m,
                Description = "Premium organic coffee beans, 1kg package",
                Category = ProductCategory.Food,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Programming Book - Clean Code",
                Price = 39.99m,
                Description = "A handbook of agile software craftsmanship",
                Category = ProductCategory.Books,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Product
            {
                Id = _nextId++,
                Name = "LED Desk Lamp",
                Price = 45.99m,
                Description = "Adjustable LED desk lamp with touch control",
                Category = ProductCategory.Home,
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                UpdatedAt = DateTime.UtcNow.AddDays(-8)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Yoga Mat",
                Price = 24.99m,
                Description = "Non-slip yoga mat with carrying strap",
                Category = ProductCategory.Sports,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Building Blocks Set",
                Price = 49.99m,
                Description = "Educational building blocks for kids, 500 pieces",
                Category = ProductCategory.Toys,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Moisturizing Cream",
                Price = 34.99m,
                Description = "Hydrating face cream with SPF 30",
                Category = ProductCategory.Beauty,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Car Phone Holder",
                Price = 12.99m,
                Description = "Universal car phone mount with 360-degree rotation",
                Category = ProductCategory.Automotive,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Product
            {
                Id = _nextId++,
                Name = "Bluetooth Speaker",
                Price = 59.99m,
                Description = "Portable waterproof Bluetooth speaker",
                Category = ProductCategory.Electronics,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = _nextId++,
                Name = "Running Shoes",
                Price = 89.99m,
                Description = "Lightweight running shoes with excellent cushioning",
                Category = ProductCategory.Sports,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        _products.AddRange(seedProducts);
    }

    public PaginatedResponse<Product> GetAll(int pageNumber, int pageSize, string? category, decimal? minPrice, decimal? maxPrice, string? sortBy, string? search)
    {
        var query = _products.AsQueryable();

        // Filtering by category
        if (!string.IsNullOrWhiteSpace(category) && Enum.TryParse<ProductCategory>(category, true, out var categoryEnum))
        {
            query = query.Where(p => p.Category == categoryEnum);
        }

        // Filtering by price range
        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        // Search by name or description
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                    p.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        // Sorting
        query = sortBy?.ToLower() switch
        {
            "name" => query.OrderBy(p => p.Name),
            "name_desc" => query.OrderByDescending(p => p.Name),
            "price" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "createdat" => query.OrderBy(p => p.CreatedAt),
            "createdat_desc" => query.OrderByDescending(p => p.CreatedAt),
            _ => query.OrderBy(p => p.Id) // Default sorting
        };

        var totalRecords = query.Count();
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        var data = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResponse<Product>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product Create(ProductCreateDto dto)
    {
        var product = new Product
        {
            Id = _nextId++,
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description,
            Category = dto.Category,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _products.Add(product);
        return product;
    }

    public Product? Update(int id, ProductUpdateDto dto)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return null;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Description = dto.Description;
        product.Category = dto.Category;
        product.UpdatedAt = DateTime.UtcNow;

        return product;
    }

    public Product? Patch(int id, ProductPatchDto dto)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return null;

        if (dto.Name != null)
            product.Name = dto.Name;

        if (dto.Price.HasValue)
            product.Price = dto.Price.Value;

        if (dto.Description != null)
            product.Description = dto.Description;

        if (dto.Category.HasValue)
            product.Category = dto.Category.Value;

        product.UpdatedAt = DateTime.UtcNow;

        return product;
    }

    public bool Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return false;

        _products.Remove(product);
        return true;
    }

    public bool Exists(int id)
    {
        return _products.Any(p => p.Id == id);
    }
}


