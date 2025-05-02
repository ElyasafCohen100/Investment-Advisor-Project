using StockAdvisorBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId);
            
        Task AddTransactionAsync(TransactionModel transaction);
        Task<TransactionModel> GetTransactionByIdAsync(int id);
        Task UpdateTransactionAsync(TransactionModel transaction);
        Task DeleteTransactionAsync(int id);

        Task<List<TransactionModel>> GetAllTransactionsAsync();
    }
}
