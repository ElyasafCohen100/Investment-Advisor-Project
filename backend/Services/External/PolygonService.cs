// ╔══════════════════════════════════════════════════════════════════╗
// ║                    🌐 PolygonService.cs                          
// ║                                                                    
// ║ 💡 Purpose:                                                         
// ║    - Calls Polygon.io API to get the latest stock price.          
// ║    - Returns the latest closing price from the response.          
// ║                                                                    
// ║ 🧰 Notes:                                                           
// ║    - Uses HttpClient and System.Text.Json.                         
// ║    - The API key should be stored securely (not hardcoded).       
// ╚═════════════════════════════════════════════════════════════════╝

using System.Text.Json;

public class PolygonService
{
    // ===== HttpClient used to make API requests ===== //
    private readonly HttpClient _httpClient;

    // ===== Polygon.io API Key (💡 should be stored in secrets in real apps) ===== //
    private readonly string _apiKey = "B3oUsO0EkvpF9xzR8vq2ob4XDP4zcx80\r\n";

    // ===== Constructor with Dependency Injection ===== //
    public PolygonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // ===== Get the latest stock price for a symbol ===== //
    public async Task<double?> GetLatestPrice(string symbol)
    {
        try
        {
            // ===== Build the request URL ===== //
            var url = $"https://api.polygon.io/v2/aggs/ticker/{symbol}/prev?adjusted=true&apiKey={_apiKey}";

            // ===== Send GET request to Polygon API ===== //
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // ===== Read response content as string ===== //
            var json = await response.Content.ReadAsStringAsync();

            // ===== Parse string to JSON document ===== //
            var doc = JsonDocument.Parse(json);

            // ===== Extract closing price from results ===== //
            return doc.RootElement.GetProperty("results")[0].GetProperty("c").GetDouble();
        }
        catch
        {
            // ===== If any error occurs, return null ===== //
            return null;
        }
    }
}
