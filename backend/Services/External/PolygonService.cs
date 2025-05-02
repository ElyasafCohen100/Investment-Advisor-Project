// שימוש ב־System.Text.Json לעבודה עם JSON
using System.Text.Json;


public class PolygonService
{

    // לקוח HTTP שמשמש לשליחת בקשות API
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "B3oUsO0EkvpF9xzR8vq2ob4XDP4zcx80\r\n";


    //DEPENTENCY INJECTION
    public PolygonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<double?> GetLatestPrice(string symbol)
    {
        try
        {
            var url = $"https://api.polygon.io/v2/aggs/ticker/{symbol}/prev?adjusted=true&apiKey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // קריאת תוכן התשובה כ־string
            var json = await response.Content.ReadAsStringAsync();
            // ואז ממיר אותו ל JSON
            var doc = JsonDocument.Parse(json);


            // שליפת המחיר האחרון מתוך ה־JSON

            return doc.RootElement.GetProperty("results")[0].GetProperty("c").GetDouble();
        }
        catch
        {
            return null;
        }
    }



}
