using SwiftShop.DTOs;

namespace SwiftShop.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(Guid userId);
        Task<CartItemResponseDTO> AddItemToCartAsync(CartItemDTO dto);
        Task<CartItemResponseDTO?> UpdateCartItemAsync(Guid itemId, CartItemUpdateDTO dto);
        Task<bool> RemoveCartItemAsync(Guid itemId);
        Task<CartItemResponseDTO?> GetCartItemAsync(Guid itemId);
        Task<CartReportDto?> GetCartReportAsync(Guid cartId);
    }
}
