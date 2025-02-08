using MilkMan.Domain.Entities;
using MilkMan.Shared.Enums;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;
using MilkMan.Application.Parameters;
using Microsoft.EntityFrameworkCore;

namespace MilkMan.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly MilkManDbContext _dbContext;

    public OrderRepository(MilkManDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId, bool trackChanges)
    {

        return await GetAllAsync(trackChanges ,o => o.CustomerId == customerId, [o => o.Items]);
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, bool trackChanges)
    {
        return await GetByIdAsync(orderId, trackChanges, includes:
        [
            o => o.Items,
            o => o.Customer
        ]);
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status, bool trackChanges)
    {
        return await GetAllAsync(trackChanges, order => order.Status == status, [o => o.Items]);
    }

    public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate, bool trackChanges)
    {
        return await GetAllAsync(trackChanges ,o => o.CreatedAt >= startDate && o.CreatedAt <= endDate, [o => o.Items]);
    }

    public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetOrdersAsync(OrderQueryParameters queryParams)
    {
        var query = _dbContext.Orders.AsNoTracking().AsQueryable();

        if (queryParams.Status.HasValue)
        {
            query = query.Where(o => o.Status == queryParams.Status.Value);
        }

        if (queryParams.StartDate.HasValue)
        {
            query = query.Where(o => o.CreatedAt >= queryParams.StartDate.Value);
        }

        if (queryParams.EndDate.HasValue)
        {
            query = query.Where(o => o.CreatedAt <= queryParams.EndDate.Value);
        }

        var totalCount = query.Count();

        var orders = await query
            .OrderByDescending(o => o.CreatedAt)
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Include(o => o.Customer)
            .ToListAsync();

        return (orders, totalCount);
    }

    public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
    {
        return await _dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<bool> UpdateOrderItemsAsync(int orderId, IEnumerable<OrderItem> newItems)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return false;
        }

        // Remove existing items
        order.Items.Clear();

        // Add new items
        order.Items = newItems.ToList();

        // Recalculate total amount
        order.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);

        return true;
    }
    public async Task AddOrderAsync(Order order)
    {
        await AddAsync(order);
    }

    public Task UpdateOrderAsync(Order order)
    {
         Update(order);
        return Task.CompletedTask;
    }

    public Task DeleteOrderAsync(Order order)
    {
        DeleteAsync(order);
        return Task.CompletedTask;
    }

    public async Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
    {
        var order = await GetByIdAsync(orderId, true);
        if (order != null)
        {
            order.Status = newStatus;
            Update(order);
        }
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
    {
        var orders = await GetAllAsync(false, o => o.CreatedAt >= startDate && o.CreatedAt <= endDate);
        return orders.Sum(o => o.TotalAmount);
    }

    public async Task<IEnumerable<Order>> GetUnshippedOrdersAsync(bool trackChanges)
    {
        return await GetAllAsync(trackChanges, o => o.Status == OrderStatus.Processing, [o => o.Items]);
    }

    public async Task<IEnumerable<Order>> GetOrdersWithItemAsync(int productId, bool trackChanges)
    {
        return await GetAllAsync(trackChanges, o => o.Items.Any(oi => oi.ProductID == productId), [o => o.Items]);
    }
}

