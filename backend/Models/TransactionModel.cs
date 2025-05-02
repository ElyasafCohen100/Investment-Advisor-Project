namespace StockAdvisorBackend.Models
{
    public class TransactionModel /// Represents a transaction made by a user, either a purchase or a sale of a stock.
        //כל פעולה של קנייה/מכירה נרשמת פה – קריטי בשביל Event Sourcing וגרפים.
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int StockId { get; set; }

        public DateTime TransactionDate { get; set; }
        public int TransactionAmount { get; set; }
        public decimal PriceAtTransaction { get; set; } // public bool IsPurchase { get; set; } // true = קניה, false = מכירה


        // public UserModel User { get; set; }
        public StockModel Stock { get; set; }

        public string TransactionType { get; set; } // "Buy" or "Sell"
    }
}
