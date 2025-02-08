using AutoMapper;
using MilkMan.Shared.DTOs.ShoppingCart;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.Common;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;

namespace MilkMan.Application.Services;

public class ShoppingCartService : IShoppingCartService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ShoppingCartService(IUnitOfWork unitOfWork, IMapper mapper, IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productService = productService;
    }
    public async Task<Result> AddItemToCart(int customerId, AddCartItemDto addCartItemDto)
    {
        // Retrieve the shopping cart for the customer
        var shoppingCart = await _unitOfWork.ShoppingCarts.GetCartByCustomererIdAsync(customerId);

        if (shoppingCart == null)
        {
            // Create a new shopping cart if it doesn't exist
            shoppingCart = new ShoppingCart { CustomerId = customerId };
            await _unitOfWork.ShoppingCarts.CreateShoppingCartAsync(shoppingCart);
            await _unitOfWork.CompleteAsync();
        }

        // Retrieve the product
        var product = await _unitOfWork.Products.GetByIdAsync(addCartItemDto.ProductId, false);

        if (product == null)
        {
            return Result.Failure("Product Not Found!");
        }

        // Check if cart items exist before accessing them (potential improvement)
        if (shoppingCart.CartItems != null)
        {
            var cartItem = shoppingCart.CartItems.FirstOrDefault(i => i.ProductId == addCartItemDto.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += addCartItemDto.Quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = addCartItemDto.ProductId,
                    Quantity = addCartItemDto.Quantity
                };
                shoppingCart.CartItems.Add(cartItem);
            }
        }
        await _unitOfWork.CompleteAsync();
        return Result.Success();
    }


    public async Task<Result> ClearCart(int customerId)
    {
        var shoppingCart = await _unitOfWork.ShoppingCarts.GetCartByCustomererIdAsync(customerId);
        if (shoppingCart == null)
        {
            return Result.Failure("ShoppingCart not found!");
        }
        
        await _unitOfWork.ShoppingCarts.ClearCartAsync(customerId);
        await _unitOfWork.CompleteAsync();
        return Result.Success();
    }

    public async Task<ShoppingCartDto?> GetCartByCustomerId(int customerId)
    {
        var shoppingCart = await _unitOfWork.ShoppingCarts.GetCartByCustomererIdAsync(customerId);

        return _mapper.Map<ShoppingCartDto>(shoppingCart);
    }

    public async Task<Result> RemoveItemFromCart(int customerId, int itemId)
    {
        var shoppingCart = await _unitOfWork.ShoppingCarts.GetCartByCustomererIdAsync(customerId);
        if (shoppingCart == null)
        {
            return Result.Failure("ShoppingCart not found!");
        }

        var cartItem = shoppingCart.CartItems.FirstOrDefault(x => x.Id == itemId);
        if (cartItem == null)
            return Result.Failure("CartItem not found!");

        await _unitOfWork.ShoppingCarts.RemoveItemFromCartAsync(customerId, itemId);
        await _unitOfWork.CompleteAsync();
        return Result.Success();
    }

    public async Task<Result<CartItemDto>> UpdateCartItemQuantityAsync(int customerId, UpdateCartItemQuantity cartItemDto)
    {
        var shoppingCart = await _unitOfWork.ShoppingCarts.GetCartByCustomererIdAsync(customerId);
        if (shoppingCart == null)
        {
            return Result<CartItemDto>.Failure("Shopping Cart not found!");
        }

        var cartItem = shoppingCart.CartItems.FirstOrDefault(x => x.Id == cartItemDto.Id);
        if (cartItem == null)
            return Result<CartItemDto>.Failure("Cart Item not found!");

  
        cartItem.Quantity = cartItemDto.Quantity;
        await _unitOfWork.CompleteAsync();

        return Result<CartItemDto>.Success(_mapper.Map<CartItemDto>(cartItem));
    }
}
