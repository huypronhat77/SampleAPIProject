using Microsoft.OpenApi.Models;
using SampleAPI.Middleware;
using SampleAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register application services
builder.Services.AddSingleton<IProductService, ProductService>();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Sample Products API", 
        Version = "v1",
        Description = "A comprehensive RESTful API for learning and testing with Postman",
        Contact = new OpenApiContact
        {
            Name = "API Support",
            Email = "support@sampleapi.com"
        }
    });
    
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-API-Key",
        Description = "API Key needed to access the endpoints"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable Swagger in all environments (useful for production API documentation)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Products API v1");
    options.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");

// Custom authorization middleware
app.UseMiddleware<AuthorizationMiddleware>();

app.MapControllers();

// Health check endpoint
app.MapGet("/", () => new
{
    Status = "Running",
    Message = "Sample Products API is running successfully!",
    Version = "1.0",
    Endpoints = new
    {
        Swagger = "/swagger",
        Products = "/api/products"
    }
}).WithTags("Health");

app.Run();
