
// ╔════════════════════════════════════════════════════════════════════════════╗
// ║                   📁 IPortfolioRepository Interface                       
// ║  Defines the contract for managing user's stock portfolio in the DB      
// ║  Includes methods for fetch, add, update, and remove portfolio items      
// ╚════════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task AddPortfolioItemAsync(PortfolioModel item);
        Task UpdatePortfolioItemAsync(PortfolioModel item);
        Task RemovePortfolioItemAsync(int userId, int stockId);
        Task<List<PortfolioModel>> GetPortfolioByUserIdAsync(int userId);
        Task<PortfolioModel> GetPortfolioItemAsync(int userId, int stockId);
    }
}
