// ╔═════════════════════════════════════════════════════════════════════╗
// ║                      💼 Portfolio Model                              
// ║  Represents a stock the user owns, including quantity and details   
// ╚═════════════════════════════════════════════════════════════════════╝

namespace StockAdvisorBackend.Models
{
    public class PortfolioModel 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int PortfolioQuantity { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public StockModel Stock { get; set; }
    }
}
