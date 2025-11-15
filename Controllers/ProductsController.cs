using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using SampleAPI.Services;
using SampleAPI.Validators;

namespace SampleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get all products with pagination, filtering, sorting, and search
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<Product>>), StatusCodes.Status200OK)]
    public IActionResult GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? category = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? search = null)
    {
        _logger.LogInformation("Getting all products - Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1 || pageSize > 100)
            pageSize = 10;

        var result = _productService.GetAll(pageNumber, pageSize, category, minPrice, maxPrice, sortBy, search);
        
        return Ok(ApiResponse<PaginatedResponse<Product>>.SuccessResponse(
            result, 
            "Products retrieved successfully"));
    }

    /// <summary>
    /// Get a product by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        _logger.LogInformation("Getting product with ID: {ProductId}", id);

        var product = _productService.GetById(id);
        
        if (product == null)
        {
            return NotFound(ApiResponse<object>.ErrorResponse($"Product with ID {id} not found."));
        }

        return Ok(ApiResponse<Product>.SuccessResponse(product, "Product retrieved successfully"));
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] ProductCreateDto dto)
    {
        _logger.LogInformation("Creating new product: {ProductName}", dto.Name);

        var validationErrors = ProductValidator.ValidateCreate(dto);
        
        if (validationErrors.Any())
        {
            return BadRequest(ApiResponse<object>.ErrorResponse("Validation failed.", validationErrors));
        }

        var product = _productService.Create(dto);
        
        return CreatedAtAction(
            nameof(GetById), 
            new { id = product.Id }, 
            ApiResponse<Product>.SuccessResponse(product, "Product created successfully"));
    }

    /// <summary>
    /// Fully update a product
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] ProductUpdateDto dto)
    {
        _logger.LogInformation("Updating product with ID: {ProductId}", id);

        var validationErrors = ProductValidator.ValidateUpdate(dto);
        
        if (validationErrors.Any())
        {
            return BadRequest(ApiResponse<object>.ErrorResponse("Validation failed.", validationErrors));
        }

        var product = _productService.Update(id, dto);
        
        if (product == null)
        {
            return NotFound(ApiResponse<object>.ErrorResponse($"Product with ID {id} not found."));
        }

        return Ok(ApiResponse<Product>.SuccessResponse(product, "Product updated successfully"));
    }

    /// <summary>
    /// Partially update a product
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public IActionResult Patch(int id, [FromBody] ProductPatchDto dto)
    {
        _logger.LogInformation("Partially updating product with ID: {ProductId}", id);

        var validationErrors = ProductValidator.ValidatePatch(dto);
        
        if (validationErrors.Any())
        {
            return BadRequest(ApiResponse<object>.ErrorResponse("Validation failed.", validationErrors));
        }

        var product = _productService.Patch(id, dto);
        
        if (product == null)
        {
            return NotFound(ApiResponse<object>.ErrorResponse($"Product with ID {id} not found."));
        }

        return Ok(ApiResponse<Product>.SuccessResponse(product, "Product partially updated successfully"));
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deleting product with ID: {ProductId}", id);

        var result = _productService.Delete(id);
        
        if (!result)
        {
            return NotFound(ApiResponse<object>.ErrorResponse($"Product with ID {id} not found."));
        }

        return NoContent();
    }

    /// <summary>
    /// Check if a product exists (HEAD request)
    /// </summary>
    [HttpHead("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Head(int id)
    {
        _logger.LogInformation("Checking existence of product with ID: {ProductId}", id);

        var exists = _productService.Exists(id);
        
        return exists ? Ok() : NotFound();
    }

    /// <summary>
    /// Get allowed HTTP methods for the products endpoint
    /// </summary>
    [HttpOptions]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Options()
    {
        Response.Headers.Append("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
        return Ok();
    }
}

