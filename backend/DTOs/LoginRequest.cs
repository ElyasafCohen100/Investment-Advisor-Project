using System.ComponentModel.DataAnnotations;

namespace StockAdvisorBackend.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username must be up to 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be up to 100 characters")]
        public string Password { get; set; }
    }
}
