using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.DTOs;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Implementations;
using StockAdvisorBackend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Controllers
{
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

        // קבלת כל העסקאות
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        // קבלת עסקה לפי ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound("Transaction not found.");
            return Ok(transaction);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(int userId)
        {
            var transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);

            if (transactions == null || transactions.Count == 0)
                return NotFound("No transactions found for this user.");

            return Ok(transactions);
        }


        // הוספת עסקה חדשה
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



            // ⬇️ עדכון תיק השקעות רק בעסקת קנייה
            if (request.TransactionType.ToLower() == "buy")
            {
                var existingItem = await _portfolioService.GetPortfolioItemAsync(request.UserId, request.StockId);

                if (existingItem != null)
                {
                    // חישוב כמות ומחיר ממוצע חדש
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
                    // אין פריט קיים – יצירה חדשה
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
            else if (request.TransactionType.ToLower() == "sell")
            {
                var existingItem = await _portfolioService.GetPortfolioItemAsync(request.UserId, request.StockId);

                if (existingItem == null)
                {
                    return BadRequest("Cannot sell a stock you don't own.");
                }

                if (existingItem.PortfolioQuantity < request.TransactionAmount)
                {
                    return BadRequest("Not enough shares to sell.");
                }

                existingItem.PortfolioQuantity -= request.TransactionAmount;

                if (existingItem.PortfolioQuantity == 0)
                {
                    await _portfolioService.RemovePortfolioItemAsync(request.UserId, request.StockId);
                }
                else
                {
                    await _portfolioService.UpdatePortfolioItemAsync(existingItem);

                }
            }


            return Ok("Transaction created successfully!");
        }

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
    

        // מחיקת עסקה
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
