// ╔═══════════════════════════════════════════════════════════════════╗
// ║                   💼 PortfolioService.cs                           
// ║                                                                      
// ║ 💡 Purpose:                                                           
// ║   - Provides business logic for portfolio operations.                
// ║   - Connects the controller to the repository layer.                
// ║                                                                      
// ║ 🧰 Used by: PortfolioController to manage user's portfolio.           
// ╚═══════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        // ======= Repository to access portfolio data ======= //
        private readonly IPortfolioRepository _portfolioItemRepository;

        // ======= Constructor for Dependency Injection ======= //
        public PortfolioService(IPortfolioRepository portfolioItemRepository)
        {
            _portfolioItemRepository = portfolioItemRepository;
        }

        // ======= Get full portfolio for a user ======= //
        public async Task<List<PortfolioModel>> GetPortfolioByUserIdAsync(int userId)
        {
            return await _portfolioItemRepository.GetPortfolioByUserIdAsync(userId);
        }

        // ======= Get specific portfolio item (by stockId) ======= //
        public async Task<PortfolioModel> GetPortfolioItemAsync(int userId, int stockId)
        {
            return await _portfolioItemRepository.GetPortfolioItemAsync(userId, stockId);
        }

        // ======= Add a new stock to user's portfolio ======= //
        public async Task AddPortfolioItemAsync(PortfolioModel item)
        {
            await _portfolioItemRepository.AddPortfolioItemAsync(item);
        }

        // ======= Update an existing stock in the portfolio ======= //
        public async Task UpdatePortfolioItemAsync(PortfolioModel item)
        {
            await _portfolioItemRepository.UpdatePortfolioItemAsync(item);
        }

        // ======= Remove a stock from user's portfolio ======= //
        public async Task RemovePortfolioItemAsync(int userId, int stockId)
        {
            await _portfolioItemRepository.RemovePortfolioItemAsync(userId, stockId);
        }
    }
}
