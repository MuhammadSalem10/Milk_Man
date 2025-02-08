using MilkMan.Shared.DTOs.Order;
using MilkMan.Shared.Common;
using MilkMan.Shared.Enums;
using MilkMan.Application.Parameters;

namespace MilkMan.Application.Interfaces;

public interface IOrderService
{
    public Task<Result<OrderDto>> PlaceOrder(PlaceOrderDto placeOrderDto);
    public Task<IEnumerable<OrderDto>> GetOrdersByCustomersId(int customerId, bool trackChanges);
    public Task<OrderDto?> GetOrderById(int orderId, bool trackChanges);
    public Task<Result> UpdateOrderStatus(int orderId, OrderStatus newStatus);
    Task<Result> CancelOrder(int orderId);
    Task<Result<PagedResult<OrderDto>>> GetOrders(OrderQueryParameters queryParams);
    Task<Result> ModifyOrder(int orderId, ModifyOrderDto modifyOrderDto);
    Task<IEnumerable<OrderDto>> GetYesterdaysOrders();
    Task<Result> MarkOrderAsDelivered(int orderId);
    Task<Result> UpdateOrderStatusBasedOnDelivery(int orderId, DeliveryStatus deliveryStatus);
}

