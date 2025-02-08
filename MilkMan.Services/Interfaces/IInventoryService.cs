using MilkMan.Shared.Common;
using MilkMan.Shared.DTOs.Inventory;


namespace MilkMan.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<Result<InventoryDto>> GetInventoryById(int id);
        Task<Result<IEnumerable<InventoryDto>>> GetAllInventory();
        Task<Result<InventoryDto>> CreateInventory(CreateInventoryDto inventoryDto);
        Task<Result<InventoryDto>> UpdateInventory(int id, UpdateInventoryDto inventoryDto);
        Task<Result> DeleteInventory(int id);
        Task<Result<InventoryDto>> GetInventoryByProductId(int productId);
        Task<Result<IEnumerable<InventoryDto>>> GetLowStockInventory();
        Task<Result<IEnumerable<InventoryDto>>> GetExpiringInventory(int daysThreshold);
        Task<Result> IncreaseInventoryQuantity(int productId, double quantityChange);
        Task<Result> DecreaseInventoryQuantity(int productId, double quantityChange);
        Task<Result<bool>> CheckProductAvailability(int productId, double requiredQuantity);
        //Task<Result<InventoryReportDto>> GenerateInventoryReportAsync();
    }

}
