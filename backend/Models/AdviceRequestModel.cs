// ╔════════════════════════════════════════════════════╗
// ║             📦 Advice Request Model                 
// ║  Represents a stock advice request from the user    
// ╚════════════════════════════════════════════════════╝

namespace StockAdvisorBackend.Models
{
    public class AdviceRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Question { get; set; }
        public string? Response { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserModel? User { get; set; }
    }
}
