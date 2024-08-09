using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Models
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, ErrorMessage = "Product Name cannot exceed 100 characters.")]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unite Price must be greater 0.")]
        public decimal UnitPrice { get; set; }
        public bool IsDeleted { get; set; }
    }
}