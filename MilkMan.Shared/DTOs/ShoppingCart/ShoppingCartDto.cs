

namespace MilkMan.Shared.DTOs.ShoppingCart
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
