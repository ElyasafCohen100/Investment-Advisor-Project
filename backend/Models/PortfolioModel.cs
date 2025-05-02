namespace StockAdvisorBackend.Models
{
    public class PortfolioModel // Represents an item in a user's portfolio, which includes the stock and the quantity owned.
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }

        public int PortfolioQuantity { get; set; }
        public decimal AveragePurchasePrice { get; set; } // מחיר קניה ממוצע

       // public UserModel User { get; set; }
        public StockModel Stock { get; set; }
    }
}
