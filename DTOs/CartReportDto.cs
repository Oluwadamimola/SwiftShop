namespace SwiftShop.DTOs
{
    public class CartReportDto
    {
        public Guid CartId { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemResponseDTO> Items { get; set; } = new List<CartItemResponseDTO>();
    }
}
