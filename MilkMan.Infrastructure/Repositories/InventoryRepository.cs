using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;


namespace MilkMan.Infrastructure.Repositories
{
    // Infrastructure/Repositories/InventoryRepository.cs
    public class InventoryRepository : IInventoryRepository
    {
        private readonly MilkManDbContext _context;
        private readonly DbSet<Inventory> _inventories;

        public InventoryRepository(MilkManDbContext context)
        {
            _context = context;
            _inventories = context.Set<Inventory>();
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _inventories
                .Include(i => i.Product)
                .ToListAsync();
        }

        public async Task<Inventory?> GetByProductIdAsync(int productId)
        {
            return await _inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.ProductId == productId);
        }

        public async Task<IEnumerable<Inventory>> GetLowStockInventoryAsync()
        {
            return await _inventories
                .Include(i => i.Product)
                .Where(i => i.Quantity <= i.ReorderPoint)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetExpiringInventoryAsync(int daysThreshold)
        {
            var thresholdDate = DateTime.UtcNow.AddDays(daysThreshold);
            return await _inventories
                .Include(i => i.Product)
                .Where(i => i.ExpiryDate.HasValue && i.ExpiryDate <= thresholdDate)
                .ToListAsync();
        }

        public async Task<Inventory?> AddAsync(Inventory inventory)
        {
            await _inventories.AddAsync(inventory);
            return inventory;
        }

        public Task Update(Inventory inventory)
        {
            _inventories.Update(inventory);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await _inventories.FindAsync(id);
            if (inventory != null)
            {
                _inventories.Remove(inventory);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _inventories.AnyAsync(i => i.Id == id);
        }
    }
}
