using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;

namespace MilkMan.Infrastructure.Repositories
{
    public class RefundRepository : Repository<Refund>, IRefundRepository
    {
        public RefundRepository(MilkManDbContext context) : base(context)
        {
        }

        public async Task<Refund> GetRefundByReturnRequestIdAsync(int returnRequestId, bool trackChanges)
        {
            return (Refund)await GetAllAsync(trackChanges, r => r.ReturnRequestId == returnRequestId);
                
        }
    }
}
