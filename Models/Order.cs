namespace OrderManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}