// ╔═════════════════════════════════════════════════════════════════════════════╗
// ║                            🚀 Program.cs (Startup)
// ║
// ║  💡 Purpose:                                                                   
// ║     Configures the ASP.NET Core Web API application pipeline, registers       
// ║     services, middleware, CORS, Swagger, and dependency injection.           
// ║                                                                                
// ║  🧰 Tech:                                                                     
// ║     - ASP.NET Core                                                            
// ║     - Entity Framework Core                                                    
// ║     - Dependency Injection                                                     
// ║     - CORS policy setup                                                        
// ╚═════════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Data;
using StockAdvisorBackend.Services;
using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Repositories.Interfaces;
using StockAdvisorBackend.Services.Implementations;
using StockAdvisorBackend.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ═════ Register application services and dependencies ═════ //
builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAdviceRequestRepository, AdviceRequestRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAdviceRequestService, AdviceRequestService>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<StockRepository>(); // optional duplicate if used elsewhere

// ═════ Register HttpClient for external services ═════ //
builder.Services.AddHttpClient<PolygonService>();

// ═════ Configure database connection using EF Core ═════ //
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ═════ Enable Swagger (OpenAPI) docs ═════ //
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ═════ Enable CORS for cross-origin requests ═════ //
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

// ═════ Use custom error-handling middleware ═════ //
app.UseMiddleware<StockAdvisorBackend.Middlewares.ErrorHandlingMiddleware>();

// ═════ Enable Swagger in development ═════ //
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ═════ Enable CORS and Authorization ===== //
app.UseCors("AllowAll");
app.UseAuthorization();

// ═════ Map controller routes ===== //
app.MapControllers();

// ═════ Start the application ===== //
app.Run();
