using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;
using MilkMan.Shared.Enums;
using System.Linq.Expressions;


namespace MilkMan.Infrastructure.Repositories;

public class DriverRepository : Repository<Driver>, IDriverRepository
{
    public DriverRepository(MilkManDbContext context) : base(context)
    {
    }

    public async Task<Driver?> GetDriverWithOrdersAsync(int id)
    {
        return await GetByIdAsync(id, false, null, new Expression<Func<Driver, object>>[] { d => d.AssignedOrders });
    }

    public async Task<IEnumerable<Driver>> GetAvailableDriversAsync()
    {
        return await GetAllAsync(false, d => d.AssignedOrders.All(o => o.Status == OrderStatus.Delivered));
    }
}
