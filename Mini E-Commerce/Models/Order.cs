namespace Mini_E_Commerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public double discounts { get; set; }
        public List<OrderItem> Items { get; set; } = new();

    }
}
