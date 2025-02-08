
using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;

namespace MilkMan.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly MilkManDbContext _dbContext;

        public ShoppingCartRepository(MilkManDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
        }
        public async Task ClearCartAsync(int customerId)
        {

            var shoppingCart = await _dbContext.ShoppingCarts.Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.CustomerId == customerId) ?? throw new Exception("Cart not found");

            shoppingCart.CartItems.Clear();



        }

        public async Task<ShoppingCart?> GetCartByCustomererIdAsync(int customerId)
        {
            return await _dbContext.ShoppingCarts
                    .Include(sc => sc.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.Unit)
                    .FirstOrDefaultAsync(sc => sc.CustomerId == customerId);
        }


        public async Task RemoveItemFromCartAsync(int customerId, int itemId)
        {
            var shoppingCart = await _dbContext.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.CustomerId == customerId) ?? throw new Exception("Can't Remove Item! Cart is not found!");

            var item = shoppingCart.CartItems.FirstOrDefault(i => i.Id == itemId) ?? throw new Exception("Item not found in the shoppingCart");
            shoppingCart.CartItems.Remove(item);
        }

       
    }
}
