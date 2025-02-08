

using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;
using MilkMan.Shared.Enums;
using MilkMan.Shared.Parameters;

namespace MilkMan.Infrastructure.Repositories;

public class DeliveryRepository : Repository<Delivery>, IDeliveryRepository
{
    public DeliveryRepository(MilkManDbContext context) : base(context) { }

    public async Task<Delivery?> GetDeliveryWithDetailsAsync(int id, bool trackChanges)
    {
        return await GetByIdAsync(id, trackChanges,
            includes:
            [
                d => d.Order,
                d => d.Driver,
                d => d.Feedback
            ]);
    }

    public async Task<IEnumerable<Delivery>> GetDeliveriesByDriverAsync(int driverId, bool trackChanges)
    {
        return await GetAllAsync(trackChanges,
            filter: d => d.DriverId == driverId,
            includes:
            [
                d => d.Order,
                d => d.Feedback
            ]);
    }

    public async Task<IEnumerable<Delivery>> GetUnassignedDeliveriesAsync(bool trackChanges)
    {
        return await GetAllAsync(trackChanges,
            filter: d => d.DriverId == null && d.Status == DeliveryStatus.Pending,
            includes:
            [
                d => d.Order
            ]);
    }

    public async Task<Delivery> GetByOrderIdAsync(int orderId, bool trackChanges)
    {
        return (Delivery)await GetAllAsync(trackChanges, d => d.OrderId == orderId);
    }

    

    public async Task<(IEnumerable<Delivery> deliveries, int totalCount)> GetDeliveriesAsync(DeliveryQueryParameters queryParams)
    {
        var query = _context.Set<Delivery>().AsQueryable();

        if (queryParams.Status.HasValue)
        {
            query = query.Where(d => d.Status == queryParams.Status.Value);
        }

        if (queryParams.DriverId.HasValue)
        {
            query = query.Where(d => d.DriverId == queryParams.DriverId.Value);
        }

        var totalCount = await query.CountAsync();

        var deliveries = await query
            .OrderBy(d => d.CreatedAt)
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();

        return (deliveries, totalCount);
    }
}