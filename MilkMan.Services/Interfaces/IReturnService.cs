using MilkMan.Domain.Entities;
using MilkMan.Shared.Common;
using MilkMan.Shared.Enums;


namespace MilkMan.Application.Interfaces
{
    public interface IReturnService
    {
        Task<Result<ReturnRequest>> CreateReturnRequestAsync(ReturnRequest returnRequest);
        Task<Result<ReturnRequest>> GetReturnRequestAsync(int id);
        Task<IEnumerable<ReturnRequest>> GetCustomerReturnRequestsAsync(int customerId);
        Task<Result> UpdateReturnRequestStatusAsync(int id, ReturnStatus status);
        Task<Result> AssignDriverToReturnRequestAsync(int returnRequestId, int driverId);
        Task<Result> UpdateReturnRequestPickupStatusAsync(int returnRequestId, ReturnStatus status, DateTime pickupDate);
        Task<IEnumerable<ReturnRequest>> GetDriverAssignedReturnRequestsAsync(int driverId);
    }
}
