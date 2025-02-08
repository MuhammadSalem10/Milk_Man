using MilkMan.Domain.Entities;
using MilkMan.Shared.Common;

public interface IRefundService
{
    Task<Result<Refund>> ProcessRefundAsync(int returnRequestId);
    Task<Result<Refund>> GetRefundByReturnRequestIdAsync(int returnRequestId);
}