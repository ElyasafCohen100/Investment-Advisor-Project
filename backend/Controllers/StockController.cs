// ╔════════════════════════════════════════════════════════════════════════════╗
// ║                         📈 StockController.cs
// ║
// ║  💡 Purpose:                                                                  
// ║     Provides CRUD operations for stock data, including integration with       
// ║     external Polygon API to fetch real-time prices.                           
// ║                                                                                
// ║  🧰 Tech:                                                                     
// ║     - ASP.NET Core Web API                                                    
// ║     - IStockService & PolygonService                                         
// ║     - DTOs for input validation                                               
// ╚════════════════════════════════════════════════════════════════════════════╝

using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.DTOs;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;

namespace StockAdvisorBackend.Controllers
{
    // ======= Route: api/Stock ======= //
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly PolygonService _polygonService;

        // ======= Constructor to inject services ======= //
        public StockController(IStockService stockService, PolygonService polygonService)
        {
            _stockService = stockService;
            _polygonService = polygonService;
        }

        // ======= GET: Get stock by ID ======= //
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
                return NotFound("Stock not found.");
            return Ok(stock);
        }

        // ======= GET: Get stock by symbol (use external API if needed) ======= //
        [HttpGet("symbol/{symbol}")]
        public async Task<ActionResult<StockModel>> GetStockBySymbol(string symbol)
        {
            var stock = await _stockService.GetOrFetchStockBySymbolAsync(symbol, _polygonService);
            if (stock == null)
                return NotFound("Price not available from external source");
            return Ok(stock);
        }

        // ======= GET: Get all stocks ======= //
        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockService.GetAllStocksAsync();
            return Ok(stocks);
        }

        // ======= POST: Add a new stock ======= //
        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] StockDto request)
        {
            var stock = new StockModel
            {
                Symbol = request.Symbol,
                CurrentPrice = request.CurrentPrice
            };

            await _stockService.AddStockAsync(stock);
            return Ok("Stock added successfully!");
        }

        // ======= PUT: Update an existing stock ======= //
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] StockDto request)
        {
            var stock = new StockModel
            {
                Id = id,
                Symbol = request.Symbol,
                CurrentPrice = request.CurrentPrice
            };

            await _stockService.UpdateStockAsync(stock);
            return Ok("Stock updated successfully!");
        }

        // ======= DELETE: Remove a stock by ID ======= //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);

            if (stock == null)
                return NotFound("Stock not found.");

            await _stockService.DeleteStockAsync(stock);
            return Ok("Stock deleted successfully!");
        }
    }
}
