
using AutoMapper;
using MilkMan.Shared.DTOs.Order;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.Common;
using MilkMan.Domain.Entities;
using MilkMan.Shared.Enums;
using MilkMan.Shared.Interfaces;
using MilkMan.Domain.Repositories;
using MilkMan.Application.Parameters;

namespace MilkMan.Application.Services;

public class OrderService : IOrderService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _loggerManager;
    private readonly IOrderHubClient _orderHubClient;
    private readonly IInventoryService _inventoryService;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager loggerManager,
        IOrderHubClient orderHubClient, IInventoryService inventoryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggerManager = loggerManager;
        _orderHubClient = orderHubClient;
        _inventoryService = inventoryService;
    }

    public async Task<Result<OrderDto>> PlaceOrder(PlaceOrderDto placeOrderDto)
    {
        var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(placeOrderDto.CustomerId, false);
        if (customer == null)
        {
            return Result<OrderDto>.Failure("Customer not found");
        }

        var order = new Order
        {
            CustomerId = customer.Id,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Placed,
            Items = new List<OrderItem>()
        };

        foreach (var item in placeOrderDto.Items)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId, false);
            if (product == null)
            {
                return Result<OrderDto>.Failure($"Product with ID {item.ProductId} not found");
            }

            order.Items.Add(new OrderItem
            {
                ProductID = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

        order.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);

        await _unitOfWork.Orders.AddAsync(order);

        foreach (var item in order.Items)
        {
            await _inventoryService.DecreaseInventoryQuantity(item.ProductID, item.Quantity);
        }

        await _unitOfWork.CompleteAsync();

        // Assign order to the zone based on customer's delivery location

        // Notify admin panel of the new order
        await _orderHubClient.SendOrderUpdateAsync(order);

        var orderDto = _mapper.Map<OrderDto>(order);
        return Result<OrderDto>.Success(orderDto);
    }

    private DateTime GetNextDeliveryDate()
    {
        var now = DateTime.UtcNow;
        var cairoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        var cairoTime = TimeZoneInfo.ConvertTimeFromUtc(now, cairoTimeZone);

        if (cairoTime.TimeOfDay >= new TimeSpan(0, 0, 0) && cairoTime.TimeOfDay < new TimeSpan(7, 0, 0))
        {
            // If it's between midnight and 7 AM, schedule for today at 7 AM
            return TimeZoneInfo.ConvertTimeToUtc(cairoTime.Date.AddHours(7), cairoTimeZone);
        }
        else
        {
            // Otherwise, schedule for tomorrow at 7 AM
            return TimeZoneInfo.ConvertTimeToUtc(cairoTime.Date.AddDays(1).AddHours(7), cairoTimeZone);
        }
    }
    public async Task<Result> UpdateOrderStatus(int orderId, OrderStatus newStatus)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId, true);
        if (order == null)
        {
            return Result.Failure("Order not found");
        }

        order.Status = newStatus;
        _unitOfWork.Orders.Update(order);
        await _unitOfWork.CompleteAsync();

        return Result.Success();
    }

    
    public async Task<OrderDto?> GetOrderById(int orderId)
    {
        var order = await _unitOfWork.Orders.GetOrderByIdAsync(orderId, false);
        return _mapper.Map<OrderDto?>(order);   
    }

    public async Task<IEnumerable<OrderDto>> GetYesterdaysOrders()
    {
        var today = DateTime.Today;
        var yesterday = today.AddDays(-1);

        var orders = await _unitOfWork.Orders.GetOrdersByDateRangeAsync(yesterday, today, trackChanges: false);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<Result> CancelOrder(int orderId)
    {
    var order = await _unitOfWork.Orders.GetByIdAsync(orderId, true);
    if (order == null)
    {
        return Result.Failure("Order not found");
    }

    if (order.Status != OrderStatus.Placed && order.Status != OrderStatus.Dispatched)
    {
        return Result.Failure("Cannot cancel order at this stage");
    }

    order.Status = OrderStatus.Cancelled;
    _unitOfWork.Orders.Update(order);
    await _unitOfWork.CompleteAsync();

    return Result.Success();
}

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomersId(int customerId, bool trackChanges)
    {
        IEnumerable<Order> orders = await _unitOfWork.Orders.GetOrdersByCustomerAsync(customerId, trackChanges);
        if (orders == null || !orders.Any())
        {
            return new List<OrderDto>(); 
        }

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetOrderById(int orderId, bool trackChanges)
    {
        var order = await _unitOfWork.Orders.GetOrderByIdAsync(orderId, trackChanges);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<Result<PagedResult<OrderDto>>> GetOrders(OrderQueryParameters queryParams)
    {
        var (orders, totalCount) = await _unitOfWork.Orders.GetOrdersAsync(queryParams);
        var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

        var pagedResult = new PagedResult<OrderDto>
        {
            Items = orderDtos,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };

        return Result<PagedResult<OrderDto>>.Success(pagedResult);
    }

    public async Task<Result> ModifyOrder(int orderId, ModifyOrderDto modifyOrderDto)
    {
        var order = await _unitOfWork.Orders.GetOrderWithDetailsAsync(orderId);
        if (order == null)
        {
            return Result.Failure("Order not found");
        }

        if (order.Status != OrderStatus.Placed && order.Status != OrderStatus.Delivered)
        {
            return Result.Failure("Cannot modify order at this stage");
        }

        var newItems = new List<OrderItem>();
        foreach (var item in modifyOrderDto.Items)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId, false);
            if (product == null)
            {
                return Result.Failure($"Product with ID {item.ProductId} not found");
            }

            newItems.Add(new OrderItem
            {
                ProductID = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

        var updated = await _unitOfWork.Orders.UpdateOrderItemsAsync(orderId, newItems);
        if (!updated)
        {
            return Result.Failure("Failed to update order items");
        }

        await _unitOfWork.CompleteAsync();

        return Result.Success();
    }

    public async Task<Result> MarkOrderAsDelivered(int orderId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId, trackChanges: true);
        if (order == null)
            return Result.Failure("Order not found");

        if (order.Status == OrderStatus.Delivered)
            return Result.Failure("Order is already marked as delivered");

        order.Status = OrderStatus.Delivered;

        await _unitOfWork.CompleteAsync();

        _loggerManager.Information($"Order {orderId} marked as delivered");
        return Result.Success();
    }

    public async Task<Result> UpdateOrderStatusBasedOnDelivery(int orderId, DeliveryStatus deliveryStatus)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId, true);
        if (order == null)
        {
            return Result.Failure("Order not found.");
        }

        switch (deliveryStatus)
        {
            case DeliveryStatus.Delivered:
                order.Status = OrderStatus.Delivered;
                break;
            case DeliveryStatus.Failed:
                order.Status = OrderStatus.Failed;
                break;
                // Add more cases as needed
        }

        await _unitOfWork.CompleteAsync();
        _loggerManager.Information($"Order {orderId} status updated based on delivery status: {deliveryStatus}");

        return Result.Success();
    }
}





