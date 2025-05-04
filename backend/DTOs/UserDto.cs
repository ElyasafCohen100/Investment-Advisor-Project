// ╔════════════════════════════════════════════════╗
// ║                 📦 User DTO   
// ║  Basic user data for registration or update  
// ╚════════════════════════════════════════════════╝

namespace StockAdvisorBackend.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
