# Quick Start Guide

## Running the API

1. **Start the API:**
   ```bash
   dotnet run
   ```
   The API will be available at: `http://localhost:5108`

2. **Access Swagger UI:**
   Open your browser and navigate to: `http://localhost:5108/swagger`

## Using Postman

### Option 1: Import Collection File

1. Open Postman
2. Click "Import" button (top left)
3. Select `SampleAPI.postman_collection.json`
4. (Optional) Import `SampleAPI.postman_environment.json` for environment variables
5. Start making requests!

### Option 2: Manual Setup

If you prefer to create requests manually in Postman:

**Base URL:** `http://localhost:5108`
**API Key Header:** `X-API-Key: dev-api-key-12345`

## Testing the API

### 1. Health Check (No Auth Required)
```
GET http://localhost:5108/
```

### 2. Get All Products
```
GET http://localhost:5108/api/products
Headers: X-API-Key: dev-api-key-12345
```

### 3. Get Product by ID
```
GET http://localhost:5108/api/products/1
Headers: X-API-Key: dev-api-key-12345
```

### 4. Create a Product
```
POST http://localhost:5108/api/products
Headers: 
  Content-Type: application/json
  X-API-Key: dev-api-key-12345
Body:
{
  "name": "New Product",
  "price": 99.99,
  "description": "Product description",
  "category": 0
}
```

### 5. Update a Product (Full)
```
PUT http://localhost:5108/api/products/1
Headers:
  Content-Type: application/json
  X-API-Key: dev-api-key-12345
Body:
{
  "name": "Updated Product",
  "price": 149.99,
  "description": "Updated description",
  "category": 0
}
```

### 6. Partial Update (PATCH)
```
PATCH http://localhost:5108/api/products/1
Headers:
  Content-Type: application/json
  X-API-Key: dev-api-key-12345
Body:
{
  "price": 79.99
}
```

### 7. Delete a Product
```
DELETE http://localhost:5108/api/products/1
Headers: X-API-Key: dev-api-key-12345
```

## Query Parameters Examples

### Pagination
```
GET /api/products?pageNumber=1&pageSize=5
```

### Filter by Category
```
GET /api/products?category=Electronics
```

### Filter by Price Range
```
GET /api/products?minPrice=20&maxPrice=100
```

### Search
```
GET /api/products?search=laptop
```

### Sorting
```
GET /api/products?sortBy=price_desc
```
Options: `name`, `name_desc`, `price`, `price_desc`, `createdat`, `createdat_desc`

### Combined Filters
```
GET /api/products?category=Electronics&minPrice=50&maxPrice=200&sortBy=price&pageNumber=1&pageSize=10
```

## Product Categories

Use these numeric values when creating/updating products:
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

## Testing Different HTTP Status Codes

### 200 OK
- GET existing product
- PUT/PATCH existing product

### 201 Created
- POST new product

### 204 No Content
- DELETE existing product

### 400 Bad Request
Try creating a product with invalid data:
```json
{
  "name": "AB",
  "price": -10,
  "description": "",
  "category": 0
}
```

### 401 Unauthorized
Make a request without the `X-API-Key` header or with an invalid key

### 404 Not Found
Try to GET/PUT/PATCH/DELETE a product with ID 999

## Tips for Learning

1. **Start with GET requests** to see the existing data
2. **Try creating products** to see 201 responses and Location headers
3. **Experiment with filters** to understand query parameters
4. **Test validation** by sending invalid data
5. **Test authorization** by removing or changing the API key
6. **Use Swagger UI** for interactive testing and documentation
7. **Check response headers** to see content-type, location, etc.
8. **Monitor HTTP status codes** in Postman or browser dev tools

## Troubleshooting

**Problem:** API doesn't start
- **Solution:** Make sure no other application is using port 5108

**Problem:** 401 Unauthorized error
- **Solution:** Ensure you're sending the `X-API-Key` header with value `dev-api-key-12345`

**Problem:** Can't find Swagger UI
- **Solution:** Make sure API is running and navigate to `http://localhost:5108/swagger`

**Problem:** Data resets after restart
- **Solution:** This is expected - the API uses in-memory storage

## Next Steps

1. Import the Postman collection
2. Run through all the requests in order
3. Modify requests to test different scenarios
4. Check the API responses and status codes
5. Experiment with different query parameters
6. Try invalid requests to see error handling

Happy testing! 🚀


