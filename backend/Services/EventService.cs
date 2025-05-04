// ╔═══════════════════════════════════════════════════════════════════════════════╗
// ║                              📝 EventService.cs                                     
// ║                                                                                    
// ║ 💡 Purpose:                                                                         
// ║   - Handles the logging of domain events to the database.                          
// ║   - Each event includes type, target object info, and serialized data.             
// ║                                                                                    
// ║ 🛠️ Used by: Portfolio, Stock, Transaction repositories                              
// ╚═══════════════════════════════════════════════════════════════════════════════╝

using System.Text.Json;
using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Services
{
    public class EventService
    {
        // === Access to database context === //
        private readonly ApplicationDbContext _context;

        // === Constructor for dependency injection === //
        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        // === Main method to log an event to DB === //
        public async Task LogEventAsync(string eventType, string aggregateType, int aggregateId, object eventData)
        {
            // Convert event data to JSON string
            var json = JsonSerializer.Serialize(eventData, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                WriteIndented = false
            });

            var eventModel = new EventModel
            {
                EventType = eventType,
                AggregateType = aggregateType,
                AggregateId = aggregateId,
                EventData = json,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                _context.Events.Add(eventModel);
                await _context.SaveChangesAsync(); // Save event to DB ✅
            }
            catch (Exception ex)
            {
                // Error handling with console output
                Console.WriteLine("🔥 שגיאת שמירה:");
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw; // Re-throw the error so it can be handled elsewhere if needed
            }
        }
    }
}
