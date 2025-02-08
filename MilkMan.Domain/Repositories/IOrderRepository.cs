using MilkMan.Application.Parameters;
using MilkMan.Domain.Entities;
using MilkMan.Shared.Enums;
namespace MilkMan.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId, bool trackChanges);
    Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status, bool trackChanges);
    Task<(IEnumerable<Order> Orders, int TotalCount)> GetOrdersAsync(OrderQueryParameters queryParams);
    Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate, bool trackChanges);
    Task<Order?> GetOrderWithDetailsAsync(int orderId);
    Task<Order?> GetOrderByIdAsync(int orderId, bool trackChanges);
    Task<bool> UpdateOrderItemsAsync(int orderId, IEnumerable<OrderItem> newItems);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Order order);
    Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Order>> GetUnshippedOrdersAsync(bool trackChanges);
    Task<IEnumerable<Order>> GetOrdersWithItemAsync(int productId, bool trackChanges);
}

