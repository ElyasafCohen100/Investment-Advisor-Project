// ╔══════════════════════════════════════════════════════════════════════╗
// ║                🤖 AdviceRequestService.cs                            
// ║                                                                      
// ║ 💡 Purpose:                                                           
// ║   - Handles logic for managing advice requests.                      
// ║   - Works with the repository to access the database.               
// ║                                                                      
// ║ 🧰 Used by: Controller to handle logic between controller & DB.       
// ╚══════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Services.Implementations
{
    public class AdviceRequestService : IAdviceRequestService
    {
        // ======= Repository to access the DB ======= //
        private readonly IAdviceRequestRepository _adviceRequestRepository;

        // ======= Constructor for Dependency Injection ======= //
        public AdviceRequestService(IAdviceRequestRepository adviceRequestRepository)
        {
            _adviceRequestRepository = adviceRequestRepository;
        }

        // ======= Get a single advice request by ID ======= //
        public async Task<AdviceRequestModel> GetAdviceRequestByIdAsync(int id)
        {
            return await _adviceRequestRepository.GetAdviceRequestByIdAsync(id);
        }

        // ======= Get all advice requests for a specific user ======= //
        public async Task<List<AdviceRequestModel>> GetAdviceRequestsByUserIdAsync(int userId)
        {
            return await _adviceRequestRepository.GetAdviceRequestsByUserIdAsync(userId);
        }

        // ======= Add a new advice request to the database ======= //
        public async Task AddAdviceRequestAsync(AdviceRequestModel request)
        {
            await _adviceRequestRepository.AddAdviceRequestAsync(request);
        }
    }
}
