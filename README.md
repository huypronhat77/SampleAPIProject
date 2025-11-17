# Sample Products API

A comprehensive RESTful API for learning and testing with Postman. This API demonstrates all HTTP methods, proper status codes, validation, pagination, filtering, sorting, and authorization.

## Features

- ✅ Full CRUD operations (Create, Read, Update, Delete)
- ✅ All HTTP methods (GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS)
- ✅ Comprehensive HTTP status codes (200, 201, 204, 400, 401, 404, 500)
- ✅ Input validation with detailed error messages
- ✅ Pagination support
- ✅ Filtering by category and price range
- ✅ Sorting by multiple fields
- ✅ Search functionality
- ✅ Simple API key authorization
- ✅ In-memory data storage (no database required)
- ✅ Swagger/OpenAPI documentation
- ✅ Global exception handling
- ✅ CORS support

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later

### Running the API

1. Navigate to the project directory
2. Run the application:

```bash
dotnet run
```

3. The API will start on `http://localhost:5108` (or `https://localhost:7278` for HTTPS)

### Accessing Swagger Documentation

Once the API is running, open your browser and navigate to:
- http://localhost:5108/swagger

## API Configuration

### API Key

The API uses a simple API key authentication. The key is configured in `appsettings.json`:

**Development Environment:**
- API Key: `dev-api-key-12345`

**Production Environment:**
- API Key: `my-secret-api-key-12345`

Include the API key in the `X-API-Key` header for all requests (except health check and Swagger endpoints).

## Using Postman

### Import the Collection

1. Open Postman
2. Click "Import" button
3. Select the `SampleAPI.postman_collection.json` file
4. The collection includes all endpoints with sample requests

### Collection Variables

The collection comes with pre-configured variables:
- `baseUrl`: `http://localhost:5108`
- `apiKey`: `dev-api-key-12345`

You can modify these in the collection variables or create a new environment.

### Available Endpoints

#### Health Check
- `GET /` - API health status (no authentication required)

#### Products
- `GET /api/products` - Get all products (with pagination, filtering, sorting, search)
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Full update of a product
- `PATCH /api/products/{id}` - Partial update of a product
- `DELETE /api/products/{id}` - Delete a product
- `HEAD /api/products/{id}` - Check if product exists
- `OPTIONS /api/products` - Get allowed HTTP methods

## Query Parameters

### Pagination
- `pageNumber` (default: 1) - Page number
- `pageSize` (default: 10, max: 100) - Number of items per page

### Filtering
- `category` - Filter by category (Electronics, Clothing, Food, Books, Home, Sports, Toys, Beauty, Automotive, Other)
- `minPrice` - Minimum price
- `maxPrice` - Maximum price

### Sorting
- `sortBy` - Sort field with options:
  - `name` - Sort by name (ascending)
  - `name_desc` - Sort by name (descending)
  - `price` - Sort by price (ascending)
  - `price_desc` - Sort by price (descending)
  - `createdat` - Sort by creation date (ascending)
  - `createdat_desc` - Sort by creation date (descending)

### Search
- `search` - Search in product name and description

## Product Model

```json
{
  "id": 1,
  "name": "Product Name",
  "price": 99.99,
  "description": "Product description",
  "category": 0,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

### Product Categories (Enum)
- 0: Electronics
- 1: Clothing
- 2: Food
- 3: Books
- 4: Home
- 5: Sports
- 6: Toys
- 7: Beauty
- 8: Automotive
- 9: Other

## Validation Rules

### Creating/Updating Products
- **Name**: Required, 3-100 characters
- **Price**: Required, must be greater than 0
- **Category**: Required, must be a valid enum value
- **Description**: Optional, maximum 500 characters

## HTTP Status Codes

The API uses proper HTTP status codes:

- `200 OK` - Successful GET, PUT, PATCH requests
- `201 Created` - Successful POST request (includes Location header)
- `204 No Content` - Successful DELETE request
- `400 Bad Request` - Validation errors
- `401 Unauthorized` - Missing or invalid API key
- `404 Not Found` - Resource not found
- `405 Method Not Allowed` - Invalid HTTP method
- `500 Internal Server Error` - Unhandled exceptions

## Example Requests

### Create a Product

```bash
curl -X POST http://localhost:5108/api/products \
  -H "Content-Type: application/json" \
  -H "X-API-Key: dev-api-key-12345" \
  -d '{
    "name": "New Product",
    "price": 29.99,
    "description": "Product description",
    "category": 0
  }'
```

### Get All Products with Filtering

```bash
curl -X GET "http://localhost:5108/api/products?category=Electronics&minPrice=20&maxPrice=100&sortBy=price&pageNumber=1&pageSize=10" \
  -H "X-API-Key: dev-api-key-12345"
```

### Update Product (Full)

```bash
curl -X PUT http://localhost:5108/api/products/1 \
  -H "Content-Type: application/json" \
  -H "X-API-Key: dev-api-key-12345" \
  -d '{
    "name": "Updated Product",
    "price": 39.99,
    "description": "Updated description",
    "category": 0
  }'
```

### Partial Update (PATCH)

```bash
curl -X PATCH http://localhost:5108/api/products/1 \
  -H "Content-Type: application/json" \
  -H "X-API-Key: dev-api-key-12345" \
  -d '{
    "price": 49.99
  }'
```

### Delete Product

```bash
curl -X DELETE http://localhost:5108/api/products/1 \
  -H "X-API-Key: dev-api-key-12345"
```

## Response Format

### Success Response

```json
{
  "success": true,
  "message": "Products retrieved successfully",
  "data": { ... },
  "errors": null
}
```

### Error Response

```json
{
  "success": false,
  "message": "Validation failed.",
  "data": null,
  "errors": [
    "Name must be between 3 and 100 characters.",
    "Price must be greater than 0."
  ]
}
```

### Paginated Response

```json
{
  "success": true,
  "message": "Products retrieved successfully",
  "data": {
    "data": [ ... ],
    "pageNumber": 1,
    "pageSize": 10,
    "totalRecords": 50,
    "totalPages": 5,
    "hasPreviousPage": false,
    "hasNextPage": true
  }
}
```

## Project Structure

```
SampleAPI/
├── Controllers/           # API endpoints
│   └── ProductsController.cs
├── Models/               # Data models and DTOs
│   ├── Product.cs
│   ├── ProductCreateDto.cs
│   ├── ProductUpdateDto.cs
│   ├── ProductPatchDto.cs
│   ├── ApiResponse.cs
│   └── PaginatedResponse.cs
├── Services/             # Business logic
│   ├── IProductService.cs
│   └── ProductService.cs
├── Validators/           # Input validation
│   └── ProductValidator.cs
├── Middleware/           # Custom middleware
│   ├── AuthorizationMiddleware.cs
│   └── ExceptionHandlingMiddleware.cs
├── Program.cs           # Application entry point
├── appsettings.json     # Configuration
└── SampleAPI.postman_collection.json  # Postman collection
```

## Learning Resources

This API is designed to help you learn:
1. RESTful API design principles
2. HTTP methods and status codes
3. Request/response patterns
4. API authentication
5. Input validation
6. Error handling
7. Pagination and filtering
8. API testing with Postman

## Notes

- This API uses in-memory storage, so all data is reset when the application restarts
- The API includes 12 pre-seeded products for testing
- All endpoints (except health check and Swagger) require the `X-API-Key` header

## Deployment

### Deploy to Render

This project is ready to deploy to Render with Docker support. See the comprehensive [Deployment Guide](DEPLOYMENT.md) for step-by-step instructions.

**Quick Deploy:**
1. Push your code to GitHub/GitLab/Bitbucket
2. Create a new Web Service on Render (select Docker runtime)
3. Set environment variables (see DEPLOYMENT.md)
4. Deploy and access your API at `https://your-service.onrender.com`

**Files for Deployment:**
- `Dockerfile` - Multi-stage Docker build configuration
- `.dockerignore` - Excludes unnecessary files from Docker image
- `render.yaml` - Infrastructure as Code for one-click deployment
- `appsettings.Production.json` - Production environment configuration

For detailed instructions, troubleshooting, and best practices, see [DEPLOYMENT.md](DEPLOYMENT.md).

## Support

For issues or questions, please refer to the Swagger documentation at `/swagger` or review the Postman collection examples.

