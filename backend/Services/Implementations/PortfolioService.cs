using StockAdvisorBackend.Models;
using StockAdvisorBackend.Repositories.Interfaces;
using StockAdvisorBackend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioItemRepository;

        public PortfolioService(IPortfolioRepository portfolioItemRepository)
        {
            _portfolioItemRepository = portfolioItemRepository;
        }

        public async Task<List<PortfolioModel>> GetPortfolioByUserIdAsync(int userId) // לשלוף את כל התיק של משתמש.
        {
            return await _portfolioItemRepository.GetPortfolioByUserIdAsync(userId);
        }

        public async Task<PortfolioModel> GetPortfolioItemAsync(int userId, int stockId) // לשלוף פריט תיק מסוים של משתמש.לשלוף פריט ספציפי בתיק (לפי StockId).
        {
            return await _portfolioItemRepository.GetPortfolioItemAsync(userId, stockId);
        }

        public async Task AddPortfolioItemAsync(PortfolioModel item) // להוסיף מניה   למשתמש.
        {
            await _portfolioItemRepository.AddPortfolioItemAsync(item);
        }

        public async Task UpdatePortfolioItemAsync(PortfolioModel item) // לעדכן מניה בתיק (למשל להגדיל כמות).
        {
            await _portfolioItemRepository.UpdatePortfolioItemAsync(item);
        }

        public async Task RemovePortfolioItemAsync(int userId, int stockId) // למחוק מניה מהתיק.
        {
            await _portfolioItemRepository.RemovePortfolioItemAsync(userId, stockId);
        }
    }
}
