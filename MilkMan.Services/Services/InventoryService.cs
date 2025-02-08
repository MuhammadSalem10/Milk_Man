using AutoMapper;
using Microsoft.Extensions.Logging;
using MilkMan.Application.Interfaces;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.Common;
using MilkMan.Shared.DTOs.Inventory;
using System.Linq.Expressions;


namespace MilkMan.Application.Services
{
    // Application/Services/InventoryService.cs
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<InventoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<InventoryDto>> GetInventoryById(int id)
        {
            var inventory = await _unitOfWork.Inventory.GetByIdAsync(id);
            if (inventory == null)
            {
                return Result<InventoryDto>.Failure($"Inventory with id {id} not found.");
            }
            return Result<InventoryDto>.Success(_mapper.Map<InventoryDto>(inventory));
        }

        public async Task<Result<IEnumerable<InventoryDto>>> GetAllInventory()
        {
            var Inventory = await _unitOfWork.Inventory.GetAllAsync();
            return Result<IEnumerable<InventoryDto>>.Success(_mapper.Map<IEnumerable<InventoryDto>>(Inventory));
        }

        public async Task<Result<InventoryDto>> CreateInventory(CreateInventoryDto inventoryDto)
        {
            var inventory = _mapper.Map<Inventory>(inventoryDto);
            await _unitOfWork.Inventory.AddAsync(inventory);
            await _unitOfWork.CompleteAsync();
            return Result<InventoryDto>.Success(_mapper.Map<InventoryDto>(inventory));
        }

        public async Task<Result<InventoryDto>> UpdateInventory(int id, UpdateInventoryDto inventoryDto)
        {
            var inventory = await _unitOfWork.Inventory.GetByIdAsync(id);
            if (inventory == null)
            {
                return Result<InventoryDto>.Failure($"Inventory with id {id} not found.");
            }

            _mapper.Map(inventoryDto, inventory);
            inventory.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.Inventory.Update(inventory);
            await _unitOfWork.CompleteAsync();
            return Result<InventoryDto>.Success(_mapper.Map<InventoryDto>(inventory));
        }

        public async Task<Result> DeleteInventory(int id)
        {
            if (!await _unitOfWork.Inventory.ExistsAsync(id))
            {
                return Result.Failure($"Inventory with id {id} not found.");
            }

            await _unitOfWork.Inventory.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }

        public async Task<Result<InventoryDto>> GetInventoryByProductId(int productId)
        {
            var inventory = await _unitOfWork.Inventory.GetByProductIdAsync(productId);
            if (inventory == null)
            {
                return Result<InventoryDto>.Failure($"Inventory for product with id {productId} not found.");
            }
            return Result<InventoryDto>.Success(_mapper.Map<InventoryDto>(inventory));
        }

        public async Task<Result<IEnumerable<InventoryDto>>> GetLowStockInventory()
        {
            var lowStockInventory = await _unitOfWork.Inventory.GetLowStockInventoryAsync();
            return Result<IEnumerable<InventoryDto>>.Success(_mapper.Map<IEnumerable<InventoryDto>>(lowStockInventory));
        }

        public async Task<Result<IEnumerable<InventoryDto>>> GetExpiringInventory(int daysThreshold)
        {
            var expiringInventory = await _unitOfWork.Inventory.GetExpiringInventoryAsync(daysThreshold);
            return Result<IEnumerable<InventoryDto>>.Success(_mapper.Map<IEnumerable<InventoryDto>>(expiringInventory));
        }

        public async Task<Result> IncreaseInventoryQuantity(int productId, double quantityChange)
        {
            var inventory = await _unitOfWork.Inventory.GetByProductIdAsync(productId);
            if (inventory == null)
            {
                return Result.Failure($"Inventory for product with id {productId} not found.");
            }

            inventory.Quantity += quantityChange;
            inventory.ModifiedAt = DateTime.UtcNow;

            await _unitOfWork.Inventory.Update(inventory);
            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }

        public async Task<Result> DecreaseInventoryQuantity(int productId, double quantityChange)
        {
            var inventory = await _unitOfWork.Inventory.GetByProductIdAsync(productId);
            if (inventory == null)
            {
                return Result.Failure($"Inventory for product with id {productId} not found.");
            }

            inventory.Quantity -= quantityChange;

            if (inventory.Quantity < 0)
            {
                return Result.Failure($"Insufficient inventory for product with id {productId}.");
            }

            await _unitOfWork.Inventory.Update(inventory);
            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }

        public async Task<Result<bool>> CheckProductAvailability(int productId, double requiredQuantity)
        {
            var inventory = await _unitOfWork.Inventory.GetByProductIdAsync(productId);
            if (inventory == null)
            {
                return Result<bool>.Failure($"Inventory for product with id {productId} not found.");
            }

            return Result<bool>.Success(inventory.Quantity >= requiredQuantity);
        }

        /* public async Task<Result<InventoryReportDto>> GenerateInventoryReportAsync()
         {
             var Inventory = await _unitOfWork.Inventory.GetAllAsync(false, null, new Expression<Func<Inventory, object>>[] { i => i.Product });
             var report = new InventoryReportDto
             {
                 TotalInventoryValue = Inventory.Sum(i => i.Quantity * (i.PurchasePrice ?? 0)),
                 LowStockItems = Inventory.Where(i => i.Quantity <= i.ReorderPoint).Select(i => _mapper.Map<InventoryDto>(i)).ToList(),
                 ExpiringItems = Inventory.Where(i => i.ExpiryDate.HasValue && i.ExpiryDate.Value <= DateTime.UtcNow.AddDays(30)).Select(i => _mapper.Map<InventoryDto>(i)).ToList(),
                 InventoryTurnover = CalculateInventoryTurnover(Inventory),
                 PopularProducts = GetPopularProducts(Inventory)
             };

             return Result<InventoryReportDto>.Success(report);
         }*/

        private decimal CalculateInventoryTurnover(IEnumerable<Inventory> Inventory)
        {
            // Implement inventory turnover calculation logic
            // This is a placeholder implementation
            return 0;
        }

        /*  private List<PopularProductDto> GetPopularProducts(IEnumerable<Inventory> Inventory)
          {
              // Implement popular products calculation logic
              // This is a placeholder implementation
              return new List<PopularProductDto>();
          }*/
    }

}