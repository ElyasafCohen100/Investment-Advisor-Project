using StockAdvisorBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId);
        Task AddTransactionAsync(TransactionModel transaction);
        Task<TransactionModel> GetTransactionByIdAsync(int id); // פונקציה לחיפוש עסקה לפי ID
        Task UpdateTransactionAsync(TransactionModel transaction); // פונקציה לעדכון עסקה
        Task DeleteTransactionAsync(int id); // פונקציה למחיקת עסקה

        Task<List<TransactionModel>> GetAllTransactionsAsync();
    }
}
