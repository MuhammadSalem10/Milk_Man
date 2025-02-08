using Microsoft.AspNetCore.Mvc;
using MilkMan.Shared.DTOs.ShoppingCart;
using MilkMan.Application.Interfaces;


namespace MilkMan.API.Controllers;

    public class ShoppingCartController : ApiController
{
    private readonly IShoppingCartService _shoppingCartService;
    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCart(int customerId)
    {
        var cart = await _shoppingCartService.GetCartByCustomerId(customerId);
        return Ok(cart);
    }

    [HttpPost("{customerId}/items")]
    public async Task<IActionResult> AddItem(int customerId, [FromBody] AddCartItemDto addCartItemDto)
    {
        
        var addItemResult = await _shoppingCartService.AddItemToCart(customerId, addCartItemDto);
        if(addItemResult.IsSuccess)
        {
        return Ok(new { message = "Product added to cart successfully." });

        }
        return BadRequest(addItemResult.ErrorMessage);
    }

    [HttpDelete("{customerId}/items/{itemId}")]
    public async Task<IActionResult> RemoveItem(int customerId, int itemId)
    {
        var removeItemResult = await _shoppingCartService.RemoveItemFromCart(customerId, itemId);
        if(removeItemResult.IsSuccess)
        {
            return Ok();
        }
        return NoContent();
    }

    [HttpPatch("{customerId}/items/{itemId}")]
    public async Task<IActionResult> UpdateCartItemQuantity([FromRoute] int customerId, [FromRoute] int itemId, [FromBody] UpdateCartItemQuantity updateCartItem)
    {
        if(itemId == 0 || itemId != updateCartItem.Id) 
        {
            return BadRequest("Invalid Item Id!");
        }
        var result = await _shoppingCartService.UpdateCartItemQuantityAsync(customerId, updateCartItem);
        if(result.IsSuccess)
        {
            return Ok(result.Value);
        } 

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("{customerId}/clear")]
    public async Task<IActionResult> ClearCart(int customerId)
    {
        var clearCartResult = await _shoppingCartService.ClearCart(customerId);
        if(clearCartResult.IsSuccess)
        {
            return Ok(new { Message = "Cart Cleared Successfully!"});
        }

        return NoContent();
    }
}
