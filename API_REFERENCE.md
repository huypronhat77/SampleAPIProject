# API Reference

## Base URL
```
http://localhost:5108
```

## Authentication
All endpoints (except `/` and `/swagger`) require API key authentication.

**Header:**
```
X-API-Key: dev-api-key-12345
```

---

## Endpoints

### Health Check

#### GET /
Check API health status (no authentication required)

**Response:** `200 OK`
```json
{
  "status": "Running",
  "message": "Sample Products API is running successfully!",
  "version": "1.0",
  "endpoints": {
    "swagger": "/swagger",
    "products": "/api/products"
  }
}
```

---

### Products

#### GET /api/products
Get all products with optional filtering, pagination, sorting, and search

**Query Parameters:**
- `pageNumber` (int, default: 1) - Page number
- `pageSize` (int, default: 10, max: 100) - Items per page
- `category` (string) - Filter by category (Electronics, Clothing, Food, Books, Home, Sports, Toys, Beauty, Automotive, Other)
- `minPrice` (decimal) - Minimum price filter
- `maxPrice` (decimal) - Maximum price filter
- `sortBy` (string) - Sort field (name, name_desc, price, price_desc, createdat, createdat_desc)
- `search` (string) - Search in name and description

**Response:** `200 OK`
```json
{
  "success": true,
  "message": "Products retrieved successfully",
  "data": {
    "data": [
      {
        "id": 1,
        "name": "Laptop Dell XPS 15",
        "price": 1299.99,
        "description": "High-performance laptop with 15-inch display",
        "category": 0,
        "createdAt": "2024-01-01T00:00:00Z",
        "updatedAt": "2024-01-01T00:00:00Z"
      }
    ],
    "pageNumber": 1,
    "pageSize": 10,
    "totalRecords": 12,
    "totalPages": 2,
    "hasPreviousPage": false,
    "hasNextPage": true
  },
  "errors": null
}
```

---

#### GET /api/products/{id}
Get a specific product by ID

**Path Parameters:**
- `id` (int) - Product ID

**Response:** `200 OK` or `404 Not Found`

**Success Response:**
```json
{
  "success": true,
  "message": "Product retrieved successfully",
  "data": {
    "id": 1,
    "name": "Laptop Dell XPS 15",
    "price": 1299.99,
    "description": "High-performance laptop with 15-inch display",
    "category": 0,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T00:00:00Z"
  },
  "errors": null
}
```

**Error Response (404):**
```json
{
  "success": false,
  "message": "Product with ID 999 not found.",
  "data": null,
  "errors": null
}
```

---

#### POST /api/products
Create a new product

**Request Body:**
```json
{
  "name": "New Product",
  "price": 99.99,
  "description": "Product description",
  "category": 0
}
```

**Validation Rules:**
- `name`: Required, 3-100 characters
- `price`: Required, must be > 0
- `category`: Required, valid enum value (0-9)
- `description`: Optional, max 500 characters

**Response:** `201 Created` or `400 Bad Request`

**Success Response:**
```json
{
  "success": true,
  "message": "Product created successfully",
  "data": {
    "id": 13,
    "name": "New Product",
    "price": 99.99,
    "description": "Product description",
    "category": 0,
    "createdAt": "2024-01-01T12:00:00Z",
    "updatedAt": "2024-01-01T12:00:00Z"
  },
  "errors": null
}
```

**Headers:**
- `Location: /api/products/13`

**Error Response (400):**
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

---

#### PUT /api/products/{id}
Fully update a product (all fields required)

**Path Parameters:**
- `id` (int) - Product ID

**Request Body:**
```json
{
  "name": "Updated Product",
  "price": 149.99,
  "description": "Updated description",
  "category": 0
}
```

**Response:** `200 OK`, `400 Bad Request`, or `404 Not Found`

**Success Response:**
```json
{
  "success": true,
  "message": "Product updated successfully",
  "data": {
    "id": 1,
    "name": "Updated Product",
    "price": 149.99,
    "description": "Updated description",
    "category": 0,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T12:30:00Z"
  },
  "errors": null
}
```

---

#### PATCH /api/products/{id}
Partially update a product (only specified fields are updated)

**Path Parameters:**
- `id` (int) - Product ID

**Request Body (all fields optional):**
```json
{
  "price": 79.99
}
```

or

```json
{
  "name": "New Name",
  "price": 129.99,
  "description": "New description"
}
```

**Response:** `200 OK`, `400 Bad Request`, or `404 Not Found`

**Success Response:**
```json
{
  "success": true,
  "message": "Product partially updated successfully",
  "data": {
    "id": 1,
    "name": "Laptop Dell XPS 15",
    "price": 79.99,
    "description": "High-performance laptop with 15-inch display",
    "category": 0,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T13:00:00Z"
  },
  "errors": null
}
```

---

#### DELETE /api/products/{id}
Delete a product

**Path Parameters:**
- `id` (int) - Product ID

**Response:** `204 No Content` or `404 Not Found`

**Success:** No content in response body

**Error Response (404):**
```json
{
  "success": false,
  "message": "Product with ID 999 not found.",
  "data": null,
  "errors": null
}
```

---

#### HEAD /api/products/{id}
Check if a product exists (returns no body)

**Path Parameters:**
- `id` (int) - Product ID

**Response:** `200 OK` or `404 Not Found`

No response body. Check status code only.

---

#### OPTIONS /api/products
Get allowed HTTP methods

**Response:** `200 OK`

**Headers:**
- `Allow: GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS`

---

## Product Categories (Enum)

| Value | Category   |
|-------|------------|
| 0     | Electronics|
| 1     | Clothing   |
| 2     | Food       |
| 3     | Books      |
| 4     | Home       |
| 5     | Sports     |
| 6     | Toys       |
| 7     | Beauty     |
| 8     | Automotive |
| 9     | Other      |

---

## HTTP Status Codes

| Code | Description | When |
|------|-------------|------|
| 200 | OK | Successful GET, PUT, PATCH requests |
| 201 | Created | Successful POST request |
| 204 | No Content | Successful DELETE request |
| 400 | Bad Request | Validation errors |
| 401 | Unauthorized | Missing or invalid API key |
| 404 | Not Found | Resource not found |
| 405 | Method Not Allowed | Invalid HTTP method |
| 500 | Internal Server Error | Unhandled exceptions |

---

## Error Response Format

All error responses follow this format:

```json
{
  "success": false,
  "message": "Error message here",
  "data": null,
  "errors": [
    "Detailed error 1",
    "Detailed error 2"
  ]
}
```

---

## Sample Requests with curl

### Get All Products
```bash
curl -X GET "http://localhost:5108/api/products" \
  -H "X-API-Key: dev-api-key-12345"
```

### Get Products with Filters
```bash
curl -X GET "http://localhost:5108/api/products?category=Electronics&minPrice=20&maxPrice=200&sortBy=price&pageNumber=1&pageSize=5" \
  -H "X-API-Key: dev-api-key-12345"
```

### Create Product
```bash
curl -X POST "http://localhost:5108/api/products" \
  -H "Content-Type: application/json" \
  -H "X-API-Key: dev-api-key-12345" \
  -d '{
    "name": "Gaming Mouse",
    "price": 79.99,
    "description": "RGB gaming mouse with programmable buttons",
    "category": 0
  }'
```

### Update Product
```bash
curl -X PUT "http://localhost:5108/api/products/1" \
  -H "Content-Type: application/json" \
  -H "X-API-Key: dev-api-key-12345" \
  -d '{
    "name": "Updated Laptop",
    "price": 1399.99,
    "description": "Updated description",
    "category": 0
  }'
```

### Partial Update
```bash
curl -X PATCH "http://localhost:5108/api/products/1" \
  -H "Content-Type: application/json" \
  -H "X-API-Key: dev-api-key-12345" \
  -d '{
    "price": 1199.99
  }'
```

### Delete Product
```bash
curl -X DELETE "http://localhost:5108/api/products/1" \
  -H "X-API-Key: dev-api-key-12345"
```

### Check Product Exists (HEAD)
```bash
curl -I -X HEAD "http://localhost:5108/api/products/1" \
  -H "X-API-Key: dev-api-key-12345"
```

---

## Rate Limiting
Not implemented (for learning purposes)

## Versioning
Current version: v1 (no version in URL for simplicity)

## CORS
Enabled for all origins (development configuration)


