// ╔═══════════════════════════════════════════════════════════════╗
// ║                       📈 Stock Model                                
// ║  Represents a stock in the system with its basic details           
// ╚═══════════════════════════════════════════════════════════════╝

namespace StockAdvisorBackend.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
