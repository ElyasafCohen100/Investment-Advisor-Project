// ╔════════════════════════════════════════════════════════════════════════╗
// ║               🤖 IAdviceRequestService.cs                                  
// ║                                                                           
// ║ 💡 Purpose:                                                                
// ║   - Defines the contract for handling advice request logic.               
// ║   - Includes methods for adding and retrieving advice requests.           
// ║                                                                           
// ║ 📦 Implemented by: AdviceRequestService                                    
// ╚════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Services.Interfaces
{
    public interface IAdviceRequestService
    {
        Task AddAdviceRequestAsync(AdviceRequestModel request);
        Task<AdviceRequestModel> GetAdviceRequestByIdAsync(int id);
        Task<List<AdviceRequestModel>> GetAdviceRequestsByUserIdAsync(int userId);
    }
}
