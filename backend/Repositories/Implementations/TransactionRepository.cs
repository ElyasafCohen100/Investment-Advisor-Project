using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Repositories.Interfaces;
using StockAdvisorBackend.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EventService _eventService;


        public TransactionRepository(ApplicationDbContext context ,EventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        // קבלת כל העסקאות של המשתמש
        public async Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _context.Transactions
                                 .Include(t => t.Stock) // אם ברצונך להוסיף מידע על המניה
                                 .Where(t => t.UserId == userId)
                                 .ToListAsync();
        }

        // הוספת עסקה חדשה
        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();


            // טען את העסקה מחדש עם Include ל-Stock לפני שאתה שולח לאירוע
            var fullTransaction = await _context.Transactions
                .Include(t => t.Stock)
                .FirstOrDefaultAsync(t => t.Id == transaction.Id);

            // רישום האירוע
            await _eventService.LogEventAsync(
            "TransactionCreated",         // עדכון השם שיהיה יותר ברור
               "Transaction",
               transaction.Id,
              fullTransaction);
        }

        // קבלת עסקה לפי ID
        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                                 .Include(t => t.Stock) // כולל את המניה כדי שנוכל להחזיר אותה
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        // עדכון עסקה
        public async Task UpdateTransactionAsync(TransactionModel transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();


            // רישום האירוע
            await _eventService.LogEventAsync(
                "TransactionUpdated",
                 "Transaction",
                     transaction.Id,
                      transaction
    );
        }

        // מחיקת עסקה
        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                // רישום האירוע
                await _eventService.LogEventAsync(
                      "TransactionDeleted",
                      "Transaction",
                       transaction.Id,
                       transaction
        );
            }
                await _context.SaveChangesAsync();
        }

        public async Task<List<TransactionModel>> GetAllTransactionsAsync()  // הוספנו את הפונקציה הזאת
        {
            return await _context.Transactions
                                 .Include(t => t.Stock)
                                 .ToListAsync();
        }
    }
}
