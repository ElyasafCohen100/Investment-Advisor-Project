using System;

namespace StockAdvisorBackend.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string EventType { get; set; }       // למשל: "StockPurchased"
        public string AggregateType { get; set; }   // למשל: "Transaction"
        public int AggregateId { get; set; }        // מזהה הישות
        public string EventData { get; set; }       // JSON של פרטי האירוע
        public DateTime CreatedAt { get; set; }     // מתי האירוע נוצר
    }
}
