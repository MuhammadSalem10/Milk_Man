using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;


namespace MilkMan.Infrastructure.Repositories
{
    public class ReturnRequestRepository : Repository<ReturnRequest>, IReturnRequestRepository
    {
        public ReturnRequestRepository(MilkManDbContext context) : base(context)
        {
        }

        public async Task<ReturnRequest?> GetReturnRequestWithDetailsAsync(int id, bool trackChanges)
        {
            return await GetByIdAsync(id, trackChanges,
                includes: [
                r => r.Order,
                    r => r.Customer,
                    r => r.Items
                ]);
        }

        public async Task<IEnumerable<ReturnRequest>> GetReturnRequestsByCustomerAsync(int customerId, bool trackChanges)
        {
            return await GetAllAsync(trackChanges,
                filter: r => r.CustomerId == customerId,
                includes: [
                r => r.Order,
                    r => r.Items
                ]);
        }

        public async Task<IEnumerable<ReturnRequest>> GetAssignedReturnRequestsAsync(int driverId, bool trackChanges)
        {
            return await GetAllAsync(trackChanges,
                filter: r => r.AssignedDriverId == driverId,
                [r => r.Order,
                    r => r.Customer,
                    r => r.Items]
                );
        }
    }
}
