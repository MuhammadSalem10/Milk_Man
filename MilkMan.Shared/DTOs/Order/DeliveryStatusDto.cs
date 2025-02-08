

using MilkMan.Shared.Enums;

namespace MilkMan.Shared.DTOs.Order;

public class DeliveryStatusDto
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public int? DriverId { get; set; }
    public string CustomerFeedback { get; set; }
    public int? CustomerRating { get; set; }
}