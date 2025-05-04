// ╔═════════════════════════════════════════════════════════════════════════════╗
// ║                     💸 TransactionController.cs
// ║
// ║  💡 Purpose:                                                                  
// ║     Handles CRUD operations for user stock transactions.                     
// ║     Automatically updates the portfolio when a user buys or sells a stock.  
// ║                                                                              
// ║  🧰 Tech:                                                                     
// ║     - ASP.NET Core Web API                                                   
// ║     - ITransactionService & IPortfolioService                               
// ║     - Business logic to manage investments                                   
// ╚════════════════════════════════════════════════════════════════════════════╝

using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.DTOs;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;

namespace StockAdvisorBackend.Controllers
{
    // ======= Route: api/Transaction ======= //
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IPortfolioService _portfolioService;

        public TransactionController(ITransactionService transactionService, IPortfolioService portfolioService)
        {
            _transactionService = transactionService;
            _portfolioService = portfolioService;
        }

        // ======= GET: All transactions ======= //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        // ======= GET: Transaction by ID ======= //
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound("Transaction not found.");
            return Ok(transaction);
        }

        // ======= GET: Transactions by user ID ======= //
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(int userId)
        {
            var transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);

            if (transactions == null || transactions.Count == 0)
                return NotFound("No transactions found for this user.");

            return Ok(transactions);
        }

        // ======= POST: Create a new transaction ======= //
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto request)
        {
            var transaction = new TransactionModel
            {
                UserId = request.UserId,
                StockId = request.StockId,
                TransactionAmount = request.TransactionAmount,
                PriceAtTransaction = request.PriceAtTransaction,
                TransactionType = request.TransactionType,
                TransactionDate = DateTime.UtcNow
            };

            await _transactionService.AddTransactionAsync(transaction);

            // ===== BUY: Add or update portfolio ===== //
            if (request.TransactionType.ToLower() == "buy")
            {
                var existingItem = await _portfolioService.GetPortfolioItemAsync(request.UserId, request.StockId);

                if (existingItem != null)
                {
                    int newAmount = existingItem.PortfolioQuantity + request.TransactionAmount;
                    decimal newAvgPrice = (
                        (existingItem.PortfolioQuantity * existingItem.AveragePurchasePrice) +
                        (request.TransactionAmount * request.PriceAtTransaction)
                    ) / newAmount;

                    existingItem.PortfolioQuantity = newAmount;
                    existingItem.AveragePurchasePrice = newAvgPrice;

                    await _portfolioService.UpdatePortfolioItemAsync(existingItem);
                }
                else
                {
                    var newItem = new PortfolioModel
                    {
                        UserId = request.UserId,
                        StockId = request.StockId,
                        PortfolioQuantity = request.TransactionAmount,
                        AveragePurchasePrice = request.PriceAtTransaction
                    };

                    await _portfolioService.AddPortfolioItemAsync(newItem);
                }
            }
            // ===== SELL: Update or remove from portfolio ===== //
            else if (request.TransactionType.ToLower() == "sell")
            {
                var existingItem = await _portfolioService.GetPortfolioItemAsync(request.UserId, request.StockId);

                if (existingItem == null)
                    return BadRequest("Cannot sell a stock you don't own.");

                if (existingItem.PortfolioQuantity < request.TransactionAmount)
                    return BadRequest("Not enough shares to sell.");

                existingItem.PortfolioQuantity -= request.TransactionAmount;

                if (existingItem.PortfolioQuantity == 0)
                    await _portfolioService.RemovePortfolioItemAsync(request.UserId, request.StockId);
                else
                    await _portfolioService.UpdatePortfolioItemAsync(existingItem);
            }

            return Ok("Transaction created successfully!");
        }

        // ======= PUT: Update a transaction ======= //
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionDto request)
        {
            var existingTransaction = await _transactionService.GetTransactionByIdAsync(id);

            if (existingTransaction == null)
                return NotFound("Transaction not found.");

            existingTransaction.UserId = request.UserId;
            existingTransaction.StockId = request.StockId;
            existingTransaction.TransactionAmount = request.TransactionAmount;
            existingTransaction.PriceAtTransaction = request.PriceAtTransaction;
            existingTransaction.TransactionType = request.TransactionType;

            await _transactionService.UpdateTransactionAsync(existingTransaction);

            return Ok("Transaction updated successfully!");
        }

        // ======= DELETE: Delete a transaction by ID ======= //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound("Transaction not found.");

            await _transactionService.DeleteTransactionAsync(id);
            return Ok("Transaction deleted successfully!");
        }
    }
}
