using StockAdvisorBackend.Models;
using StockAdvisorBackend.Repositories.Interfaces;
using StockAdvisorBackend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // קבלת כל העסקאות של המשתמש לפי userId
        public async Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _transactionRepository.GetTransactionsByUserIdAsync(userId);
        }

        // הוספת עסקה חדשה
        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            await _transactionRepository.AddTransactionAsync(transaction);
        }

        // קבלת עסקה לפי ID
        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }

        // עדכון עסקה קיימת
        public async Task UpdateTransactionAsync(TransactionModel transaction)
        {
            await _transactionRepository.UpdateTransactionAsync(transaction);
        }

        // מחיקת עסקה
        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionRepository.DeleteTransactionAsync(id);
        }

        public async Task<List<TransactionModel>> GetAllTransactionsAsync()  // הוספנו את הפונקציה הזאת
        {
            return await _transactionRepository.GetAllTransactionsAsync();  // הנחתי שאתה מקבל את כל העיסקאות מ-Repository
        }
    }
}
