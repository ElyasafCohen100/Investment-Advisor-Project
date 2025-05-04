// ╔══════════════════════════════════════════════════════════════════════════╗
// ║                        📁 StockRepository.cs
// ║
// ║  💡 Implements: IStockRepository                                              
// ║                                                                                
// ║  ✅ Purpose:                                                                  
// ║     fetching by ID or symbol, and logging related events.                     
// ║                                                                               
// ║  🧰 Tech:                                                                      
// ║     - Entity Framework Core                                                   
// ║     - EventService for audit logging                                          
// ╚══════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services;
using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Repositories.Interfaces;


namespace StockAdvisorBackend.Repositories.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EventService _eventService;

        // ======= Constructor ======= //
        public StockRepository(ApplicationDbContext context, EventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        // ======= Get a stock by its ID ======= //
        public async Task<StockModel> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        // ======= Get all stocks from the database ======= //
        public async Task<List<StockModel>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        // ======= Add a new stock and log the creation event ======= //
        public async Task AddStockAsync(StockModel stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            await _eventService.LogEventAsync(
                "StockCreated",
                "Stock",
                stock.Id,
                stock
            );
        }

        // ======= Update an existing stock and log the update event ======= //
        public async Task UpdateStockAsync(StockModel stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            await _eventService.LogEventAsync(
                "StockUpdated",
                "Stock",
                stock.Id,
                stock
            );
        }

        // ======= Delete a stock and log the deletion event ======= //
        public async Task DeleteStockAsync(StockModel stock)
        {
            var existing = await _context.Stocks.FindAsync(stock.Id);
            if (existing != null)
            {
                _context.Stocks.Remove(existing);

                await _eventService.LogEventAsync(
                    "StockDeleted",
                    "Stock",
                    stock.Id,
                    stock
                );

                await _context.SaveChangesAsync();
            }
        }

        // ======= Get a stock by its symbol (case-insensitive) ======= //
        public async Task<StockModel> GetStockBySymbolAsync(string symbol)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
        }
    }
}
