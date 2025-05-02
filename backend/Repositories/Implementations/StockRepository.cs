using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Repositories.Interfaces;
using StockAdvisorBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Repositories.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EventService _eventService;


        public StockRepository(ApplicationDbContext context, EventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        public async Task<StockModel> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<List<StockModel>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task AddStockAsync(StockModel stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            // Trigger an event after adding a stock
            await _eventService.LogEventAsync(
                  "StockCreated",
                  "Stock",
                  stock.Id,
                  stock
    );
        }

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

        public async Task DeleteStockAsync(StockModel stock)
        {
            var stock1 = await _context.Stocks.FindAsync(stock);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);

                await _eventService.LogEventAsync(
                    "StockDeleted",
                    "Stock",
                    stock.Id,
                    stock
                );

                await _context.SaveChangesAsync();
            }
        }

        public async Task<StockModel> GetStockBySymbolAsync(string symbol)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
        }

    }
}
