

namespace MilkMan.Shared.DTOs.Order;

public class PlaceOrderDto
{
    public int CustomerId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}


