// ╔════════════════════════════════════════════════════════════════════╗
// ║                     💼 PortfolioController.cs
// ║
// ║  💡 Purpose:                                                           
// ║     Manages stock portfolios per user — supports adding stocks        
// ║     and retrieving current portfolio items.                           
// ║                                                                        
// ║  🧰 Tech:                                                              
// ║     - ASP.NET Core                                                    
// ║     - IPortfolioService (DI)                                          
// ╚════════════════════════════════════════════════════════════════════╝

using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.DTOs;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;

namespace StockAdvisorBackend.Controllers
{
    // ======= Route: api/Portfolio ======= //
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        // ======= Injected service to manage portfolio logic ======= //
        private readonly IPortfolioService _portfolioService;

        // ======= Constructor for dependency injection ======= //
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        // ======= POST: Add a stock to the user's portfolio ======= //
        [HttpPost]
        public async Task<IActionResult> AddPortfolioItem([FromBody] PortfolioDto request)
        {
            var portfolioItem = new PortfolioModel
            {
                UserId = request.UserId,
                StockId = request.StockId,
                PortfolioQuantity = request.Quantity,
                AveragePurchasePrice = request.PurchasePrice
            };

            await _portfolioService.AddPortfolioItemAsync(portfolioItem);

            return Ok("Stock added to portfolio successfully.");
        }

        // ======= GET: Get all portfolio items for a specific user ======= //
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPortfolioByUserId(int userId)
        {
            var portfolioItems = await _portfolioService.GetPortfolioByUserIdAsync(userId);

            if (portfolioItems == null || portfolioItems.Count == 0)
                return NotFound("No portfolio items found for this user.");

            var response = portfolioItems.Select(item => new
            {
                stockSymbol = item.Stock?.Symbol ?? "N/A",
                amount = item.PortfolioQuantity,
                purchasePrice = item.AveragePurchasePrice,
                value = item.PortfolioQuantity * item.AveragePurchasePrice
            });

            return Ok(new
            {
                userId = userId,
                portfolio = response
            });
        }
    }
}
