using MilkMan.Shared.DTOs.Product;


namespace MilkMan.Shared.DTOs.ShoppingCart
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }


    }
}
