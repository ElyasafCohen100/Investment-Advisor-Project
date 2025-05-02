using System.ComponentModel.DataAnnotations;

namespace StockAdvisorBackend.DTOs
{
    public class PortfolioDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "StockId is required")]
        public int StockId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Purchase price must be positive")]
        public decimal PurchasePrice { get; set; }
    }
}
