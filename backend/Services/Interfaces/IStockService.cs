using StockAdvisorBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Services.Interfaces
{
    public interface IStockService
    {

        Task AddStockAsync(StockModel stock);
        Task UpdateStockAsync(StockModel stock);
        Task DeleteStockAsync(StockModel stock); 
        Task<List<StockModel>> GetAllStocksAsync();
        Task<StockModel> GetStockByIdAsync(int id);
        Task<StockModel> GetStockBySymbolAsync(string symbol);

        Task<StockModel> GetOrFetchStockBySymbolAsync(string symbol, PolygonService polygonService);
    }
}
