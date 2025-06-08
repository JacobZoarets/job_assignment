// Program.cs
// This file is the entry point for the .NET Web API application.
// It configures services, middleware, and dependency injection for the application.

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserManagement.API.Middleware;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add controllers
builder.Services.AddControllers();

// Configure Entity Framework with SQLite (easier for development and testing)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpClient for external API calls
builder.Services.AddHttpClient<RandomUserApiService>();

// Register repository and service dependencies
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<RandomUserApiService>();

// Configure CORS to allow frontend requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000") // Next.js default ports
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configure Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Management API",
        Version = "v1",
        Description = "A comprehensive API for managing users with data sourced from Random User Generator API",
        Contact = new OpenApiContact
        {
            Name = "Developer",
            Email = "developer@example.com"
        }
    });
    
    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline

// Use custom exception middleware for global error handling
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline based on environment
if (app.Environment.IsDevelopment())
{
    // Enable Swagger in development environment
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowFrontend");

// Enable authorization (if needed in the future)
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Ensure database is created and apply migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        // Create database if it doesn't exist
        context.Database.EnsureCreated();
        
        // Alternatively, use migrations in production:
        // context.Database.Migrate();
        
        app.Logger.LogInformation("Database initialized successfully");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while initializing the database");
    }
}

// Log application startup
app.Logger.LogInformation("User Management API is starting up...");

// Run the application
app.Run();