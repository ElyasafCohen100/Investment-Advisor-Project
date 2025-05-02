using StockAdvisorBackend.Data;
using StockAdvisorBackend.Repositories.Implementations;
using StockAdvisorBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Services.Implementations;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Services;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAdviceRequestRepository, AdviceRequestRepository>();
builder.Services.AddScoped<IAdviceRequestService, AdviceRequestService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<StockRepository>();
builder.Services.AddHttpClient<PolygonService>();






// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Add CORS =====
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
app.UseMiddleware<StockAdvisorBackend.Middlewares.ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();
