using AutoMapper;
using MilkMan.Application.Interfaces;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.Common;
using MilkMan.Shared.Enums;
using MilkMan.Shared.Interfaces;

namespace MilkMan.Application.Services;

public class ReturnService : IReturnService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ReturnService(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<ReturnRequest>> CreateReturnRequestAsync(ReturnRequest returnRequest)
    {
        try
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(returnRequest.OrderId, trackChanges: false);
            if (order == null)
            {
                return Result<ReturnRequest>.Failure("Order not found");
            }

            if ((DateTime.Now - order.CreatedAt).TotalDays > 14)
            {
                return Result<ReturnRequest>.Failure("Return period has expired");
            }

            returnRequest.Status = ReturnStatus.Pending;
            returnRequest.RequestDate = DateTime.Now;

            await _unitOfWork.ReturnRequests.AddAsync(returnRequest);
            await _unitOfWork.CompleteAsync();

            _logger.Information($"Return request created for order {returnRequest.OrderId}");
            return Result<ReturnRequest>.Success(returnRequest);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error creating return request: {ex.Message}");
            return Result<ReturnRequest>.Failure("Failed to create return request");
        }
    }

    public async Task<Result<ReturnRequest>> GetReturnRequestAsync(int id)
    {
        var returnRequest = await _unitOfWork.ReturnRequests.GetReturnRequestWithDetailsAsync(id, trackChanges: false);
        return returnRequest != null
            ? Result<ReturnRequest>.Success(returnRequest)
            : Result<ReturnRequest>.Failure("Return request not found");
    }

    public async Task<IEnumerable<ReturnRequest>> GetCustomerReturnRequestsAsync(int customerId)
    {
        return await _unitOfWork.ReturnRequests.GetReturnRequestsByCustomerAsync(customerId, trackChanges: false);
    }

    public async Task<Result> UpdateReturnRequestStatusAsync(int id, ReturnStatus status)
    {
        var returnRequest = await _unitOfWork.ReturnRequests.GetByIdAsync(id, trackChanges: true);
        if (returnRequest == null)
        {
            return Result.Failure("Return request not found");
        }

        returnRequest.Status = status;
        _unitOfWork.ReturnRequests.Update(returnRequest);
        await _unitOfWork.CompleteAsync();

        _logger.Information($"Return request {id} status updated to {status}");
        return Result.Success();
    }

    public async Task<Result> AssignDriverToReturnRequestAsync(int returnRequestId, int driverId)
    {
        var returnRequest = await _unitOfWork.ReturnRequests.GetByIdAsync(returnRequestId, trackChanges: true);
        if (returnRequest == null)
        {
            return Result.Failure("Return request not found");
        }

        var driver = await _unitOfWork.Drivers.GetByIdAsync(driverId, trackChanges: false);
        if (driver == null)
        {
            return Result.Failure("Driver not found");
        }

        returnRequest.AssignedDriverId = driverId;
        returnRequest.Status = ReturnStatus.DriverAssigned;

        await _unitOfWork.CompleteAsync();

        _logger.Information($"Driver {driverId} assigned to return request {returnRequestId}");
        return Result.Success();
    }

    public async Task<Result> UpdateReturnRequestPickupStatusAsync(int returnRequestId, ReturnStatus status, DateTime pickupDate)
    {
        var returnRequest = await _unitOfWork.ReturnRequests.GetByIdAsync(returnRequestId, trackChanges: true);
        if (returnRequest == null)
        {
            return Result.Failure("Return request not found");
        }

        if (status != ReturnStatus.PickedUp && status != ReturnStatus.Completed)
        {
            return Result.Failure("Invalid status update");
        }

        returnRequest.Status = status;
        returnRequest.PickupDate = pickupDate;

        await _unitOfWork.CompleteAsync();

        _logger.Information($"Return request {returnRequestId} status updated to {status}");
        return Result.Success();
    }

    public async Task<IEnumerable<ReturnRequest>> GetDriverAssignedReturnRequestsAsync(int driverId)
    {
        return await _unitOfWork.ReturnRequests.GetAssignedReturnRequestsAsync(driverId, trackChanges: false);
    }
}
