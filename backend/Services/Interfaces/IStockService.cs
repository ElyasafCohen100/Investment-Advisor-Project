// ╔═══════════════════════════════════════════════════════════════════════════╗
// ║                    📈 IStockService.cs                                          
// ║                                                                                
// ║ 💡 Purpose:                                                                     
// ║   - Interface defining services for managing stocks.                           
// ║   - Includes DB access, updates, and external API integration (Polygon).       
// ║                                                                                
// ║ 📦 Implemented by: StockService                                              
// ╚═══════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;


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
