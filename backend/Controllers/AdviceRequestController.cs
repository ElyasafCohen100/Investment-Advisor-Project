// ╔════════════════════════════════════════════════════════════════════════════════╗
// ║                     🤖 AdviceRequestController.cs                             
// ║  💡 Purpose:                                                                  
// ║     Receives advice questions from users, sends them to Ollama LLM,          
// ║     stores the AI response in the database, and returns the result.          
// ║     Supports GET and POST endpoints.                                         
// ║                                                                              
// ║  🧰 Tech:                                                                     
// ║     - ASP.NET Core Web API                                                   
// ║     - HTTP client for AI request                                            
// ║     - Dependency Injection (AdviceRequestService)                            
// ╚════════════════════════════════════════════════════════════════════════════════╝

using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;

namespace StockAdvisorBackend.Controllers
{
    // ======== Route: api/AdviceRequest ========= //
    [ApiController]
    [Route("api/[controller]")] 
    public class AdviceRequestController : ControllerBase
    {
        // ======== Injected Service to handle DB logic ========= //
        private readonly IAdviceRequestService _adviceRequestService;

        // ======== Constructor for Dependency Injection ========= //
        public AdviceRequestController(IAdviceRequestService adviceRequestService)
        {
            _adviceRequestService = adviceRequestService;
        }

        // ======== POST: Create a new advice request ========= //
        // Receives a user question, sends it to Ollama, stores the answer
        [HttpPost]
        public async Task<IActionResult> CreateAdviceRequest([FromBody] AdviceRequestModel adviceRequest)
        {
            adviceRequest.CreatedAt = DateTime.UtcNow;

            adviceRequest.Response = await SendQuestionToOllama(adviceRequest.Question);

            await _adviceRequestService.AddAdviceRequestAsync(adviceRequest);

            return Ok(new { message = "Advice request created and answered successfully." });
        }

        // ======== GET: Get all advice requests for a specific user ========= //
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<AdviceRequestModel>>> GetAdviceRequestsByUserId(int userId)
        {
            var requests = await _adviceRequestService.GetAdviceRequestsByUserIdAsync(userId);
            return Ok(requests);
        }

        // ======== Helper: Send question to Ollama LLM ========= //
        private async Task<string> SendQuestionToOllama(string question)
        {
            using var httpClient = new HttpClient();

            var requestBody = new
            {
                model = "llama3", // Model name used by Ollama server
                prompt = question
            };

            var response = await httpClient.PostAsJsonAsync("http://localhost:11434/api/generate", requestBody);

            if (!response.IsSuccessStatusCode)
                return "Sorry, could not retrieve advice at the moment.";

            var responseContent = await response.Content.ReadFromJsonAsync<OllamaResponse>();
            return responseContent?.response ?? "No advice available.";
        }

        // ======== Helper Class to parse Ollama's response ========= //
        private class OllamaResponse
        {
            public string response { get; set; }
        }
    }
}
