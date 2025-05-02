using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace StockAdvisorBackend.Controllers
{ 


    [ApiController]
    [Route("api/[controller]")] // מסלול ה-API יהיה api/AdviceRequest
    public class AdviceRequestController : ControllerBase
    {
        private readonly IAdviceRequestService _adviceRequestService;

        // קונסטרקטור שמכניס את ה-AdviceRequestService
        public AdviceRequestController(IAdviceRequestService adviceRequestService)
        {
            _adviceRequestService = adviceRequestService;
        }

        // --- פונקציה ליצירת שאלה חדשה ---
        [HttpPost]
        public async Task<IActionResult> CreateAdviceRequest([FromBody] AdviceRequestModel adviceRequest)
        {
            // שמירת תאריך יצירת הבקשה
            adviceRequest.CreatedAt = DateTime.UtcNow;

            // שליחת השאלה לשרת Ollama וקבלת תשובה
            adviceRequest.Response = await SendQuestionToOllama(adviceRequest.Question);

            // שמירה של השאלה + התשובה במסד הנתונים (Somee)
            await _adviceRequestService.AddAdviceRequestAsync(adviceRequest);

            // החזרת תגובה חיובית ל-Frontend
            return Ok(new { message = "Advice request created and answered successfully." });
        }

        // --- פונקציה לקבל את כל השאלות של יוזר מסויים לפי ה-UserId ---
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<AdviceRequestModel>>> GetAdviceRequestsByUserId(int userId)
        {
            var requests = await _adviceRequestService.GetAdviceRequestsByUserIdAsync(userId);
            return Ok(requests);
        }

        // --- פונקציה פנימית ששולחת שאלה לשרת Ollama ---
        private async Task<string> SendQuestionToOllama(string question)
        {
            using var httpClient = new HttpClient();

            // גוף הבקשה שאותו שולחים ל-Ollama
            var requestBody = new
            {
                model = "llama3", // השם של המודל שרץ אצלך ב-Ollama
                prompt = question
            };

            // שליחת בקשה לשרת Ollama
            var response = await httpClient.PostAsJsonAsync("http://localhost:11434/api/generate", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                // במקרה של שגיאה חוזרים עם הודעה כללית
                return "Sorry, could not retrieve advice at the moment.";
            }

            // קבלת התגובה מ-Ollama
            var responseContent = await response.Content.ReadFromJsonAsync<OllamaResponse>();
            return responseContent?.response ?? "No advice available.";
        }

        // --- מחלקת עזר לקרוא את התשובה של Ollama ---
        private class OllamaResponse
        {
            public string response { get; set; }
        }
    }
}
