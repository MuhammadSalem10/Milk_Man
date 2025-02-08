using MilkMan.Domain.Entities;


namespace MilkMan.Domain.Repositories
{
    public interface IRefundRepository : IRepository<Refund>
    {
        Task<Refund> GetRefundByReturnRequestIdAsync(int returnRequestId, bool trackChanges);
    }
}
