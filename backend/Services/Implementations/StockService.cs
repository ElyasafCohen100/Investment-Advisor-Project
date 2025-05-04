// ╔══════════════════════════════════════════════════════════════════════════╗
// ║                        📦 StockService.cs                                 
// ║                                                                          
// ║ 💡 Purpose:                                                               
// ║   - Handles business logic related to stock operations.                  
// ║   - Connects the controller to the stock repository and Polygon API.     
// ║                                                                          
// ║ 🧰 Used by: StockController                                                
// ╚══════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Services.Implementations
{
    public class StockService : IStockService
    {
        // ======= Injected stock repository ======= //
        private readonly IStockRepository _stockRepository;

        // ======= Constructor ======= //
        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        // ======= Get stock by ID ======= //
        public async Task<StockModel> GetStockByIdAsync(int id)
        {
            return await _stockRepository.GetStockByIdAsync(id);
        }

        // ======= Get all stocks ======= //
        public async Task<List<StockModel>> GetAllStocksAsync()
        {
            return await _stockRepository.GetAllStocksAsync();
        }

        // ======= Add a new stock ======= //
        public async Task AddStockAsync(StockModel stock)
        {
            await _stockRepository.AddStockAsync(stock);
        }

        // ======= Update a stock ======= //
        public async Task UpdateStockAsync(StockModel stock)
        {
            await _stockRepository.UpdateStockAsync(stock);
        }

        // ======= Delete a stock ======= //
        public async Task DeleteStockAsync(StockModel stock)
        {
            await _stockRepository.DeleteStockAsync(stock);
        }

        // ======= Get stock by symbol ======= //
        public async Task<StockModel> GetStockBySymbolAsync(string symbol)
        {
            return await _stockRepository.GetStockBySymbolAsync(symbol);
        }

        // ======= Get or fetch stock from Polygon API if needed ======= //
        public async Task<StockModel> GetOrFetchStockBySymbolAsync(string symbol, PolygonService polygonService)
        {
            symbol = symbol.ToUpper();

            var stock = await _stockRepository.GetStockBySymbolAsync(symbol);

            // Check if update is needed
            var shouldUpdate = stock == null || stock.CurrentPrice <= 0 || stock.LastUpdated < DateTime.UtcNow.AddMinutes(-60);

            if (shouldUpdate)
            {
                var latestPrice = await polygonService.GetLatestPrice(symbol);
                if (latestPrice == null) return null;

                if (stock == null)
                {
                    stock = new StockModel
                    {
                        Symbol = symbol,
                        CurrentPrice = (decimal)latestPrice,
                        LastUpdated = DateTime.UtcNow
                    };
                    await _stockRepository.AddStockAsync(stock);
                }
                else
                {
                    stock.CurrentPrice = (decimal)latestPrice;
                    stock.LastUpdated = DateTime.UtcNow;
                    await _stockRepository.UpdateStockAsync(stock);
                }
            }

            return stock;
        }
    }
}
