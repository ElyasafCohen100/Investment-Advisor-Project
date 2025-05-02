using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.DTOs;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using System.Threading.Tasks;


namespace StockAdvisorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        // 🎯 הוספת מניה לתיק האישי
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

        // 🎯 שליפת כל המניות בתיק של משתמש
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
