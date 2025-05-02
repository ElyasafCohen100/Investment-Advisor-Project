namespace StockAdvisorBackend.DTOs
{
    public class TransactionDto
    {
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int TransactionAmount { get; set; }
        public decimal PriceAtTransaction { get; set; }
        public string TransactionType { get; set; } = "Buy"; // או אפשר גם בלי ברירת מחדל
    }
}
