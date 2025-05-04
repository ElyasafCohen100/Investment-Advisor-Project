// ╔══════════════════════════════════════════════════════════════════════════╗
// ║                       💡 AdviceRequestRepository.cs
// ║ 
// ║  📁 Implements: IAdviceRequestRepository                                     
// ║                                                                             
// ║  ✅ Purpose:                                                                
// ║     Handles all database operations related to user advice requests,       
// ║     including adding new requests and retrieving them by user or ID.      
// ║                                                                             
// ║  🧰 Tech: Uses Entity Framework Core for async DB access                    
// ╚══════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;
using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Repositories.Implementations
{
    public class AdviceRequestRepository : IAdviceRequestRepository
    {
        // ======= EF Core DB context ======= //
        private readonly ApplicationDbContext _context;

        // ======= Constructor with dependency injection ======= //
        public AdviceRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ======= Get a single advice request by its ID ======= //
        public async Task<AdviceRequestModel> GetAdviceRequestByIdAsync(int id)
        {
            return await _context.AdviceRequests.FindAsync(id);
        }

        // ======= Get all advice requests by user ID ======= //
        public async Task<List<AdviceRequestModel>> GetAdviceRequestsByUserIdAsync(int userId)
        {
            return await _context.AdviceRequests
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();
        }

        // ======= Add a new advice request to the database ======= //
        public async Task AddAdviceRequestAsync(AdviceRequestModel request)
        {
            _context.AdviceRequests.Add(request);
            await _context.SaveChangesAsync();
        }
    }
}
