using static SwiftShop.Models.Cart;

namespace SwiftShop.DTOs
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<CartItemDTO> Items  { get; set; } = new List<CartItemDTO>();
    }

     public class CartCreateDto
    {
        public Guid UserId { get; set; }
    }

    public class CartUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
    public class CartDeleteDto
    {
        public Guid Id { get; set; }
    }
}