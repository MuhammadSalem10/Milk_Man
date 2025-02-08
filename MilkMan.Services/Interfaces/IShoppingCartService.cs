
using MilkMan.Shared.DTOs.ShoppingCart;
using MilkMan.Shared.Common;

namespace MilkMan.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto?> GetCartByCustomerId(int customerId);
        Task<Result> AddItemToCart(int customerId, AddCartItemDto addCartItemDto);
        Task<Result<CartItemDto>> UpdateCartItemQuantityAsync(int customerId, UpdateCartItemQuantity cartItemDto);
        Task<Result> RemoveItemFromCart(int customerId, int itemId);
        Task<Result> ClearCart(int customerId);
    }
}
