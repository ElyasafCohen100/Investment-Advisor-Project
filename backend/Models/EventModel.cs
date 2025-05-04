// ╔═════════════════════════════════════════════════════════════════╗
// ║                    🎯 Event Model                                  
// ║  Represents a system event (like a stock purchase or update)      
// ╚═════════════════════════════════════════════════════════════════╝

namespace StockAdvisorBackend.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public string AggregateType { get; set; }
        public int AggregateId { get; set; }
        public string EventData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
