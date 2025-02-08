using MilkMan.Domain.Entities;


namespace MilkMan.Domain.Repositories
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetByIdAsync(int id);
        Task<IEnumerable<Inventory?>> GetAllAsync();
        Task<Inventory?> GetByProductIdAsync(int productId);
        Task<IEnumerable<Inventory>> GetLowStockInventoryAsync();
        Task<IEnumerable<Inventory>> GetExpiringInventoryAsync(int daysThreshold);
        Task<Inventory?> AddAsync(Inventory inventory);
        Task Update(Inventory inventory);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
