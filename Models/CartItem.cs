namespace SwiftShop.Models
{
    public class CartItem {
        public Guid Id {get; set;}
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Cart? Cart { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }
}