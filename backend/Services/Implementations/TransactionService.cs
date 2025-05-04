// ╔════════════════════════════════════════════════════════════════════════════╗
// ║                      💸 TransactionService.cs                              
// ║                                                                           
// ║ 💡 Purpose:                                                                
// ║   - Handles business logic for transactions (buy/sell stocks).            
// ║   - Delegates DB operations to ITransactionRepository.                    
// ║                                                                           
// ║ 🧰 Used by: TransactionController                                           
// ╚════════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        // ======= Injected repository ======= //
        private readonly ITransactionRepository _transactionRepository;

        // ======= Constructor ======= //
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // ======= Get all transactions by user ID ======= //
        public async Task<List<TransactionModel>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _transactionRepository.GetTransactionsByUserIdAsync(userId);
        }

        // ======= Add a new transaction ======= //
        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            await _transactionRepository.AddTransactionAsync(transaction);
        }

        // ======= Get transaction by ID ======= //
        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }

        // ======= Update an existing transaction ======= //
        public async Task UpdateTransactionAsync(TransactionModel transaction)
        {
            await _transactionRepository.UpdateTransactionAsync(transaction);
        }

        // ======= Delete transaction by ID ======= //
        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionRepository.DeleteTransactionAsync(id);
        }

        // ======= Get all transactions ======= //
        public async Task<List<TransactionModel>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }
    }
}
