using MilkMan.Domain.Entities;


namespace MilkMan.Domain.Repositories;

    public interface IShoppingCartRepository 
    {
        Task<ShoppingCart?> GetCartByCustomererIdAsync(int customerId);
        Task CreateShoppingCartAsync(ShoppingCart shoppingCart);
        Task RemoveItemFromCartAsync(int customerId, int itemId);
        Task ClearCartAsync(int customerId);
}


