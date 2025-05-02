using StockAdvisorBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<PortfolioModel>> GetPortfolioByUserIdAsync(int userId);
        Task<PortfolioModel> GetPortfolioItemAsync(int userId, int stockId);
        Task AddPortfolioItemAsync(PortfolioModel item);
        Task UpdatePortfolioItemAsync(PortfolioModel item);
        Task RemovePortfolioItemAsync(int userId, int stockId);
    }
}
