// ╔═════════════════════════════════════════════════════════════════════════════════╗
// ║                    💸 ITransactionService.cs                                    
// ║                                                                                 
// ║ 💡 Purpose:                                                                      
// ║   - Interface defining services for managing stock transactions.                
// ║   - Supports operations like create, fetch, update, and delete transactions.    
// ║   - Also fetches transactions by user ID and returns all transactions.          
// ║                                                                                 
// ║ 📦 Implemented by: TransactionService                                            
// ╚═════════════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;


namespace StockAdvisorBackend.Services.Interfaces
{
    public interface ITransactionService
    {
        Task DeleteTransactionAsync(int id);
        Task AddTransactionAsync(TransactionModel transaction);
        Task<TransactionModel> GetTransactionByIdAsync(int id);
        Task UpdateTransactionAsync(TransactionModel transaction);
        Task<List<TransactionModel>> GetAllTransactionsAsync();
        Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId);
    }
}
