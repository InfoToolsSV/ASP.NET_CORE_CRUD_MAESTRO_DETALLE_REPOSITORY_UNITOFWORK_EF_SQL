using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(100, ErrorMessage = "Customer Name cannot exceed 100 characters.")]
        public string CustomerName { get; set; } = null!;
        [Required(ErrorMessage = "Order Date is required")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; } = [];
    }
}