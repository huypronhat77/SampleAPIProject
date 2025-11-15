using SampleAPI.Models;

namespace SampleAPI.Services;

public interface IProductService
{
    PaginatedResponse<Product> GetAll(int pageNumber, int pageSize, string? category, decimal? minPrice, decimal? maxPrice, string? sortBy, string? search);
    Product? GetById(int id);
    Product Create(ProductCreateDto dto);
    Product? Update(int id, ProductUpdateDto dto);
    Product? Patch(int id, ProductPatchDto dto);
    bool Delete(int id);
    bool Exists(int id);
}


