using Microsoft.EntityFrameworkCore;
using SwiftShop.Data;
using SwiftShop.DTOs;
using SwiftShop.Models;

namespace SwiftShop.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    DateCreated = DateTime.UtcNow
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return new CartDto
            {
                Id = cart.Id,
                UserId = userId,
                DateCreated = cart.DateCreated,
                Items = cart.CartItems.Select(ci => new CartItemDTO
                {
                    UserId = userId,
                    ProductName = ci.ProductName,
                    Price = ci.Price,
                    Quantity = ci.Quantity
                }).ToList()
            };
        }

        public async Task<CartItemResponseDTO> AddItemToCartAsync(CartItemDTO dto)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == dto.UserId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = dto.UserId,
                    DateCreated = DateTime.UtcNow
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductName = dto.ProductName,
                Price = dto.Price,
                Quantity = dto.Quantity
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return MapToResponseDTO(cartItem);
        }

        public async Task<CartItemResponseDTO?> UpdateCartItemAsync(Guid itemId, CartItemUpdateDTO dto)
        {
            var cartItem = await _context.CartItems.FindAsync(itemId);
            if (cartItem == null)
                return null;

            cartItem.Quantity = dto.Quantity;
            cartItem.Price = dto.Price;

            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return MapToResponseDTO(cartItem);
        }

        public async Task<bool> RemoveCartItemAsync(Guid itemId)
        {
            var cartItem = await _context.CartItems.FindAsync(itemId);
            if (cartItem == null)
                return false;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartItemResponseDTO?> GetCartItemAsync(Guid itemId)
        {
            var cartItem = await _context.CartItems.FindAsync(itemId);
            if (cartItem == null)
                return null;

            return MapToResponseDTO(cartItem);
        }

        public async Task<CartReportDto?> GetCartReportAsync(Guid cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
                return null;

            return new CartReportDto
            {
                CartId = cart.Id,
                TotalItems = cart.CartItems.Sum(ci => ci.Quantity),
                TotalPrice = cart.CartItems.Sum(ci => ci.TotalPrice),
                Items = cart.CartItems.Select(ci => MapToResponseDTO(ci)).ToList()
            };
        }

        private static CartItemResponseDTO MapToResponseDTO(CartItem item)
        {
            return new CartItemResponseDTO
            {
                Id = item.Id,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice
            };
        }
    }
}
