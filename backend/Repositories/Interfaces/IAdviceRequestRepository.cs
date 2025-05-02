using StockAdvisorBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IAdviceRequestRepository
    {
        Task<AdviceRequestModel> GetAdviceRequestByIdAsync(int id);
        Task<List<AdviceRequestModel>> GetAdviceRequestsByUserIdAsync(int userId);
        Task AddAdviceRequestAsync(AdviceRequestModel request);
    }
}
