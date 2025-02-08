using MilkMan.Domain.Entities;


namespace MilkMan.Domain.Repositories
{
    public interface IReturnRequestRepository : IRepository<ReturnRequest>
    {
        Task<ReturnRequest?> GetReturnRequestWithDetailsAsync(int id, bool trackChanges);
        Task<IEnumerable<ReturnRequest>> GetReturnRequestsByCustomerAsync(int customerId, bool trackChanges);
        Task<IEnumerable<ReturnRequest>> GetAssignedReturnRequestsAsync(int driverId, bool trackChanges);
    }
}
