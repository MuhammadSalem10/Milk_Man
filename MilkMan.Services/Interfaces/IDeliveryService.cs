
using MilkMan.Shared.Common;
using MilkMan.Shared.DTOs.Delivery;
using MilkMan.Shared.DTOs.Order;
using MilkMan.Shared.Enums;
using MilkMan.Shared.Parameters;

namespace MilkMan.Application.Interfaces;


public interface IDeliveryService
{
    Task<Result<DeliveryDto>> AssignDriverToOrder(int orderId, int driverId);
    Task<Result<DeliveryDto>> MarkDeliveryAsCompleted(int deliveryId);
    Task<Result<DeliveryDto>> GetDeliveryByOrderId(int orderId);
    Task<Result<PagedResult<DeliveryDto>>> GetDeliveries(DeliveryQueryParameters queryParams);
    Task<Result<DeliveryDto>> UpdateDeliveryStatus(int deliveryId, DeliveryStatus newStatus);
}


