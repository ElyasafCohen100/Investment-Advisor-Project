// ╔═════════════════════════════════════════════════════════════════════════╗
// ║               📁 IAdviceRequestRepository Interface                     
// ║  Defines the contract for working with advice requests in the database   
// ║  Includes methods for fetching and adding advice requests                
// ╚═════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IAdviceRequestRepository
    {
        Task AddAdviceRequestAsync(AdviceRequestModel request);
        Task<AdviceRequestModel> GetAdviceRequestByIdAsync(int id);
        Task<List<AdviceRequestModel>> GetAdviceRequestsByUserIdAsync(int userId);
    }
}
