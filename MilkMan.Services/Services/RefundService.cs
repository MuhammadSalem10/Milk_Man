using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.Common;
using MilkMan.Shared.Enums;
using MilkMan.Shared.Interfaces;

namespace MilkMan.Application.Services;

public class RefundService : IRefundService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerManager _logger;

    public RefundService(IUnitOfWork unitOfWork, ILoggerManager logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Refund>> ProcessRefundAsync(int returnRequestId)
    {
        try
        {
            var returnRequest = await _unitOfWork.ReturnRequests.GetReturnRequestWithDetailsAsync(returnRequestId, trackChanges: false);
            if (returnRequest == null)
            {
                return Result<Refund>.Failure("Return request not found");
            }

            if (returnRequest.Status != ReturnStatus.Approved)
            {
                return Result<Refund>.Failure("Return request is not approved");
            }

            var refundAmount = returnRequest.Items.Sum(item => item.Product.Price * item.Quantity);
            var refund = new Refund
            {
                ReturnRequestId = returnRequestId,
                Amount = refundAmount,
                ProcessedAt = DateTime.Now,
                Status = RefundStatus.Pending
            };

            await _unitOfWork.Refunds.AddAsync(refund);
            await _unitOfWork.CompleteAsync();

            _logger.Information($"Refund processed for return request {returnRequestId}");
            return Result<Refund>.Success(refund);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error processing refund: {ex.Message}");
            return Result<Refund>.Failure("Failed to process refund");
        }
    }

    public async Task<Result<Refund>> GetRefundByReturnRequestIdAsync(int returnRequestId)
    {
        var refund = await _unitOfWork.Refunds.GetRefundByReturnRequestIdAsync(returnRequestId, trackChanges: false);
        return refund != null
            ? Result<Refund>.Success(refund)
            : Result<Refund>.Failure("Refund not found");
    }
}