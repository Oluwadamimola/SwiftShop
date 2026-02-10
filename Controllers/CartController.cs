using Microsoft.AspNetCore.Mvc;
using SwiftShop.DTOs;
using SwiftShop.Services;

namespace SwiftShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> GetCart(Guid userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<ActionResult<CartItemResponseDTO>> AddItem([FromBody] CartItemDTO dto)
        {
            var item = await _cartService.AddItemToCartAsync(dto);
            return CreatedAtAction(nameof(GetItem), new { itemId = item.Id }, item);
        }

        [HttpGet("items/{itemId}")]
        public async Task<ActionResult<CartItemResponseDTO>> GetItem(Guid itemId)
        {
            var item = await _cartService.GetCartItemAsync(itemId);
            if (item == null)
                return NotFound(new { message = "Cart item not found." });

            return Ok(item);
        }

        [HttpPut("items/{itemId}")]
        public async Task<ActionResult<CartItemResponseDTO>> UpdateItem(Guid itemId, [FromBody] CartItemUpdateDTO dto)
        {
            var item = await _cartService.UpdateCartItemAsync(itemId, dto);
            if (item == null)
                return NotFound(new { message = "Cart item not found." });

            return Ok(item);
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> RemoveItem(Guid itemId)
        {
            var removed = await _cartService.RemoveCartItemAsync(itemId);
            if (!removed)
                return NotFound(new { message = "Cart item not found." });

            return NoContent();
        }

        [HttpGet("{cartId}/report")]
        public async Task<ActionResult<CartReportDto>> GetReport(Guid cartId)
        {
            var report = await _cartService.GetCartReportAsync(cartId);
            if (report == null)
                return NotFound(new { message = "Cart not found." });

            return Ok(report);
        }
    }
}
