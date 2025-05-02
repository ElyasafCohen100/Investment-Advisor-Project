using System;
using System.Text.Json;
using System.Threading.Tasks;
using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogEventAsync(string eventType, string aggregateType, int aggregateId, object eventData)
        {
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
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 שגיאת שמירה:");
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw; 
            }
        }
    }
}
