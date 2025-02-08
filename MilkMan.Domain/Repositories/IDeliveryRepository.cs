using MilkMan.Domain.Entities;
using MilkMan.Shared.Parameters;


namespace MilkMan.Domain.Repositories;

public interface IDeliveryRepository : IRepository<Delivery>
{
    Task<Delivery> GetByOrderIdAsync(int orderId, bool trackChanges);
    Task<(IEnumerable<Delivery> deliveries, int totalCount)> GetDeliveriesAsync(DeliveryQueryParameters queryParams);
    Task<Delivery?> GetDeliveryWithDetailsAsync(int id, bool trackChanges);
    Task<IEnumerable<Delivery>> GetDeliveriesByDriverAsync(int driverId, bool trackChanges);
    Task<IEnumerable<Delivery>> GetUnassignedDeliveriesAsync(bool trackChanges);
}


