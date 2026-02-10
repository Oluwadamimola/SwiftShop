namespace SwiftShop.DTOs
{
    public class CartItemDTO
    {
        public Guid UserId { get; set; }         
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CartItemResponseDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartItemUpdateDTO
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
