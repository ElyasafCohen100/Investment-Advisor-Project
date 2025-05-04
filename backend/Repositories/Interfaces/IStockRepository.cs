// ╔═══════════════════════════════════════════════════════════════════╗
// ║                 📁 IStockRepository Interface                           
// ║  Defines the contract for handling stock data in the database            
// ║  Includes methods to fetch, add, update, and delete stocks               
// ╚═══════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task AddStockAsync(StockModel stock);
        Task UpdateStockAsync(StockModel stock);
        Task DeleteStockAsync(StockModel stock);
        Task<StockModel> GetStockByIdAsync(int id);
        Task<List<StockModel>> GetAllStocksAsync();
        Task<StockModel> GetStockBySymbolAsync(string symbol);
    }
}
