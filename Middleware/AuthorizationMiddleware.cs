using System.Text.Json;
using SampleAPI.Models;

namespace SampleAPI.Middleware;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authorization for Swagger endpoints and health checks
        if (context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path.StartsWithSegments("/health") ||
            context.Request.Path.Value == "/")
        {
            await _next(context);
            return;
        }

        var expectedApiKey = _configuration["ApiSettings:ApiKey"];
        
        if (!context.Request.Headers.TryGetValue("X-API-Key", out var providedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            
            var response = ApiResponse<object>.ErrorResponse(
                "API Key is missing. Please provide 'X-API-Key' header.",
                new List<string> { "Authentication failed: Missing API key" }
            );
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return;
        }

        if (providedApiKey != expectedApiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            
            var response = ApiResponse<object>.ErrorResponse(
                "Invalid API Key.",
                new List<string> { "Authentication failed: Invalid API key" }
            );
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return;
        }

        await _next(context);
    }
}


