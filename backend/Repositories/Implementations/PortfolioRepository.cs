// ╔════════════════════════════════════════════════════════════════════════════════╗
// ║                        📁 PortfolioRepository.cs
// ║
// ║  💡 Implements: IPortfolioRepository                                          
// ║                                                                               
// ║  ✅ Purpose:                                                                 
// ║     Handles all database operations related to user portfolios, including     
// ║     adding, updating, retrieving, and removing portfolio items.              
// ║     Also logs events using the EventService for audit and tracking.          
// ║                                                                               
// ║  🧰 Tech:                                                                      
// ║     - Entity Framework Core (async DB operations)                           
// ║     - EventService for logging state changes                                  
// ╚════════════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services;
using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Repositories.Interfaces;


namespace StockAdvisorBackend.Repositories.Implementations
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EventService _eventService; // Event logging service

        // ======= Constructor ======= //
        public PortfolioRepository(ApplicationDbContext context, EventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        // ======= Get all portfolio items for a specific user (including stock info) ======= //
        public async Task<List<PortfolioModel>> GetPortfolioByUserIdAsync(int userId)
        {
            return await _context.PortfolioItems
                                 .Include(p => p.Stock)
                                 .Where(p => p.UserId == userId)
                                 .ToListAsync();
        }

        // ======= Get a single portfolio item for a specific user and stock ======= //
        public async Task<PortfolioModel> GetPortfolioItemAsync(int userId, int stockId)
        {
            return await _context.PortfolioItems
                                 .FirstOrDefaultAsync(p => p.UserId == userId && p.StockId == stockId);
        }

        // ======= Add a new portfolio item and log the creation event ======= //
        public async Task AddPortfolioItemAsync(PortfolioModel item)
        {
            _context.PortfolioItems.Add(item);
            await _context.SaveChangesAsync();
            await _eventService.LogEventAsync(
                "PortfolioItemCreated",
                "Portfolio",
                item.UserId,
                item
            );
        }

        // ======= Update an existing portfolio item and log the update event ======= //
        public async Task UpdatePortfolioItemAsync(PortfolioModel item)
        {
            _context.PortfolioItems.Update(item);
            await _context.SaveChangesAsync();
            await _eventService.LogEventAsync(
                "PortfolioItemUpdated",
                "Portfolio",
                item.UserId,
                item
            );
        }

        // ======= Remove a portfolio item and log the deletion event ======= //
        public async Task RemovePortfolioItemAsync(int userId, int stockId)
        {
            var item = await GetPortfolioItemAsync(userId, stockId);
            if (item != null)
            {
                _context.PortfolioItems.Remove(item);
                await _context.SaveChangesAsync();
                await _eventService.LogEventAsync(
                    "PortfolioItemDeleted",
                    "Portfolio",
                    userId,
                    item
                );
            }
        }
    }
}
