using System.ComponentModel.DataAnnotations;

namespace StockAdvisorBackend.DTOs
{
    public class StockDto
    {
        [Required(ErrorMessage = "Symbol is required")]
        [StringLength(10, ErrorMessage = "Symbol must be up to 10 characters")]
        public string Symbol { get; set; }


        [Range(0.01, double.MaxValue, ErrorMessage = "Current price must be positive")]
        public decimal CurrentPrice { get; set; }
    }
}
