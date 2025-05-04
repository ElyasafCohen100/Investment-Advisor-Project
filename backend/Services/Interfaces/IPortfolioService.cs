// ╔═══════════════════════════════════════════════════════════════════════════╗
// ║                  📊 IPortfolioService.cs                                       
// ║                                                                                
// ║ 💡 Purpose:                                                                     
// ║   - Interface defining methods for portfolio operations.                       
// ║   - Handles adding, updating, deleting, and retrieving portfolio items.        
// ║                                                                                
// ║ 📦 Implemented by: PortfolioService                                             
// ╚═══════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task AddPortfolioItemAsync(PortfolioModel item);
        Task UpdatePortfolioItemAsync(PortfolioModel item);
        Task RemovePortfolioItemAsync(int userId, int stockId);
        Task<List<PortfolioModel>> GetPortfolioByUserIdAsync(int userId);
        Task<PortfolioModel> GetPortfolioItemAsync(int userId, int stockId);
    }
}
