// ╔════════════════════════════════════════════════════════════════════════╗
// ║               💰 ITransactionRepository Interface                        
// ║  Defines the contract for managing stock transactions in the database     
// ║  Includes methods to fetch, add, update, and delete transactions          
// ╚════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task DeleteTransactionAsync(int id);
        Task AddTransactionAsync(TransactionModel transaction);
        Task<TransactionModel> GetTransactionByIdAsync(int id);
        Task UpdateTransactionAsync(TransactionModel transaction);
        Task<List<TransactionModel>> GetAllTransactionsAsync();
        Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId);
    }
}
