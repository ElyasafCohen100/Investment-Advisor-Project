// ╔════════════════════════════════════════════════════════════════════╗
// ║                   💰 TransactionRepository.cs
// ║
// ║  💡 Implements: ITransactionRepository                                             
// ║                                                                                    
// ║  ✅ Purpose:                                                                      
// ║     creation, update, deletion, and retrieval by user or ID.                      
// ║     Logs events with EventService to track state changes.                         
// ║                                                                                   
// ║  🧰 Tech:                                                                          
// ║     - Entity Framework Core (async DB calls)                                      
// ║     - EventService (for logging CRUD operations)                                
// ╚════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services;
using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EventService _eventService;

        // ======= Constructor with DI ======= //
        public TransactionRepository(ApplicationDbContext context, EventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        // ======= Get all transactions by user ID (including stock info) ======= //
        public async Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _context.Transactions
                                 .Include(t => t.Stock)
                                 .Where(t => t.UserId == userId)
                                 .ToListAsync();
        }

        // ======= Add a new transaction and log event ======= //
        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Reload transaction with stock data for logging
            var fullTransaction = await _context.Transactions
                                                .Include(t => t.Stock)
                                                .FirstOrDefaultAsync(t => t.Id == transaction.Id);

            await _eventService.LogEventAsync(
                "TransactionCreated",
                "Transaction",
                transaction.Id,
                fullTransaction
            );
        }

        // ======= Get a single transaction by its ID ======= //
        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                                 .Include(t => t.Stock)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        // ======= Update an existing transaction and log event ======= //
        public async Task UpdateTransactionAsync(TransactionModel transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            await _eventService.LogEventAsync(
                "TransactionUpdated",
                "Transaction",
                transaction.Id,
                transaction
            );
        }

        // ======= Delete a transaction by ID and log event ======= //
        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);

                await _eventService.LogEventAsync(
                    "TransactionDeleted",
                    "Transaction",
                    transaction.Id,
                    transaction
                );

                await _context.SaveChangesAsync();
            }
        }

        // ======= Get all transactions in the system ======= //
        public async Task<List<TransactionModel>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                                 .Include(t => t.Stock)
                                 .ToListAsync();
        }
    }
}
