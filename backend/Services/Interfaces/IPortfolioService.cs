using StockAdvisorBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<List<PortfolioModel>> GetPortfolioByUserIdAsync(int userId);
        Task<PortfolioModel> GetPortfolioItemAsync(int userId, int stockId);
        Task AddPortfolioItemAsync(PortfolioModel item);
        Task UpdatePortfolioItemAsync(PortfolioModel item);
        Task RemovePortfolioItemAsync(int userId, int stockId);
    }
}













//using StockAdvisorBackend.Models;

//namespace StockAdvisorBackend.Services.Interfaces
//{
//    public class IPortfolioService
//    {


//        Task<List<PortfolioItem>> GetPortfolioByUserIdAsync(int userId);
//        Task<PortfolioItem> GetPortfolioItemAsync(int userId, int stockId);
//        Task AddPortfolioItemAsync(PortfolioItem item);
//        Task UpdatePortfolioItemAsync(PortfolioItem item);
//        Task RemovePortfolioItemAsync(int userId, int stockId);



//        //Task<List<Stock>> GetPortfolioByUserIdAsync(int userId);
//        //Task AddStockToPortfolioAsync(int userId, CreateStockDto stockDto);
//        //Task UpdateStockInPortfolioAsync(int userId, int stockId, UpdateStockDto stockDto);
//        //Task DeleteStockFromPortfolioAsync(int userId, int stockId);
//        //Task<decimal> CalculatePortfolioValueAsync(int userId);
//        //Task<List<AdviceRequest>> GetAdviceRequestsByUserIdAsync(int userId);
//        //Task AddAdviceRequestAsync(AdviceRequest adviceRequest);
//        //Task UpdateAdviceRequestAsync(AdviceRequest adviceRequest);
//        //Task DeleteAdviceRequestAsync(int adviceRequestId); 
//    }
//}
