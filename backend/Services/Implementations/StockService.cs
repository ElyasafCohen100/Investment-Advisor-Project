using StockAdvisorBackend.Models;
using StockAdvisorBackend.Repositories.Interfaces;
using StockAdvisorBackend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<StockModel> GetStockByIdAsync(int id)
        {
            return await _stockRepository.GetStockByIdAsync(id);
        }

        public async Task<List<StockModel>> GetAllStocksAsync()
        {
            return await _stockRepository.GetAllStocksAsync();
        }

        public async Task AddStockAsync(StockModel stock)
        {
            await _stockRepository.AddStockAsync(stock);
        }

        public async Task UpdateStockAsync(StockModel stock)
        {
            await _stockRepository.UpdateStockAsync(stock);
        }

        public async Task DeleteStockAsync(StockModel stock)
        {
            await _stockRepository.DeleteStockAsync(stock);
        }

        public async Task<StockModel> GetStockBySymbolAsync(string symbol)
        {
            return await _stockRepository.GetStockBySymbolAsync(symbol);
        }


        public async Task<StockModel> GetOrFetchStockBySymbolAsync(string symbol, PolygonService polygonService)
        {
            symbol = symbol.ToUpper();
            var stock = await _stockRepository.GetStockBySymbolAsync(symbol);

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
