using AutoMapper;
using MilkMan.Application.Interfaces;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.Common;
using MilkMan.Shared.DTOs.Delivery;
using MilkMan.Shared.DTOs.Order;
using MilkMan.Shared.Enums;
using MilkMan.Shared.Interfaces;
using MilkMan.Shared.Parameters;


namespace MilkMan.Application.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public DeliveryService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<DeliveryDto>> AssignDriverToOrder(int orderId, int driverId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId, true);
        if (order == null)
        {
            return Result<DeliveryDto>.Failure("Order not found.");
        }

        var driver = await _unitOfWork.Drivers.GetByIdAsync(driverId, true);
        if (driver == null)
        {
            return Result<DeliveryDto>.Failure("Driver not found.");
        }

        if (order.Delivery == null)
        {
            order.Delivery = new Delivery
            {
                OrderId = orderId,
                DriverId = driverId,
                Status = DeliveryStatus.Assigned,
            };
        }
        else
        {
            order.Delivery.DriverId = driverId;
            order.Delivery.Status = DeliveryStatus.Assigned;
        }

        await _unitOfWork.CompleteAsync();
        _logger.Information($"Driver {driverId} assigned to order {orderId}");

        return Result<DeliveryDto>.Success(_mapper.Map<DeliveryDto>(order.Delivery));
    }

    public async Task<Result<DeliveryDto>> MarkDeliveryAsCompleted(int deliveryId)
    {
        var delivery = await _unitOfWork.Deliveries.GetByIdAsync(deliveryId, true);
        if (delivery == null)
        {
            return Result<DeliveryDto>.Failure("Delivery not found.");
        }

        delivery.Status = DeliveryStatus.Delivered;
        delivery.DeliveryTime = DateTime.UtcNow;

        await _unitOfWork.CompleteAsync();
        _logger.Information($"Delivery {deliveryId} marked as completed");

        return Result<DeliveryDto>.Success(_mapper.Map<DeliveryDto>(delivery));
    }

    public async Task<Result<DeliveryDto>> GetDeliveryByOrderId(int orderId)
    {
        var delivery = await _unitOfWork.Deliveries.GetByOrderIdAsync(orderId, false);
        if (delivery == null)
        {
            return Result<DeliveryDto>.Failure("Delivery not found for the given order.");
        }

        return Result<DeliveryDto>.Success(_mapper.Map<DeliveryDto>(delivery));
    }

    public async Task<Result<PagedResult<DeliveryDto>>> GetDeliveries(DeliveryQueryParameters queryParams)
    {
        var (deliveries, totalCount) = await _unitOfWork.Deliveries.GetDeliveriesAsync(queryParams);
        var deliveryDtos = _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);

        var pagedResult = new PagedResult<DeliveryDto>
        {
            Items = deliveryDtos,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };

        return Result<PagedResult<DeliveryDto>>.Success(pagedResult);
    }

    public async Task<Result<DeliveryDto>> UpdateDeliveryStatus(int deliveryId, DeliveryStatus newStatus)
    {
        var delivery = await _unitOfWork.Deliveries.GetByIdAsync(deliveryId, true);
        if (delivery == null)
        {
            return Result<DeliveryDto>.Failure("Delivery not found.");
        }

        delivery.Status = newStatus;
        await _unitOfWork.CompleteAsync();
        _logger.Information($"Delivery {deliveryId} status updated to {newStatus}");

        return Result<DeliveryDto>.Success(_mapper.Map<DeliveryDto>(delivery));
    }
}
