// ╔════════════════════════════════════════════════════════════════════╗
// ║                     💸 Transaction Model                                  
// ║  Represents a user transaction: buying or selling a stock               
// ║  Important for event sourcing and generating financial charts 📊        
// ╚════════════════════════════════════════════════════════════════════╝

namespace StockAdvisorBackend.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionAmount { get; set; }
        public decimal PriceAtTransaction { get; set; }
        public StockModel Stock { get; set; }
        public string TransactionType { get; set; } // "Buy" or "Sell"
    }
}