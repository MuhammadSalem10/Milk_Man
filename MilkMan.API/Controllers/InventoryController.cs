using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.DTOs.Inventory;

namespace MilkMan.API.Controllers
{

    [Authorize(Roles = "Admin,Manager")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAllInventory()
        {
            var result = await _inventoryService.GetAllInventory();
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventory(int id)
        {
            var result = await _inventoryService.GetInventoryById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryDto>> CreateInventory(CreateInventoryDto inventoryDto)
        {
            var result = await _inventoryService.CreateInventory(inventoryDto);
            return result.IsSuccess ? CreatedAtAction(nameof(GetInventory), new { id = result.Value.Id }, result.Value) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InventoryDto>> UpdateInventory(int id, UpdateInventoryDto inventoryDto)
        {
            var result = await _inventoryService.UpdateInventory(id, inventoryDto);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            var result = await _inventoryService.DeleteInventory(id);
            return result.IsSuccess ? NoContent() : BadRequest(result.ErrorMessage);
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<InventoryDto>> GetInventoryByProductId(int productId)
        {
            var result = await _inventoryService.GetInventoryByProductId(productId);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetLowStockInventory()
        {
            var result = await _inventoryService.GetLowStockInventory();
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("expiring")]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetExpiringInventory([FromQuery] int daysThreshold = 30)
        {
            var result = await _inventoryService.GetExpiringInventory(daysThreshold);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("update-quantity/{productId}")]
        public async Task<ActionResult> UpdateInventoryQuantity(int productId, [FromBody] double quantityChange)
        {
            var result = await _inventoryService.IncreaseInventoryQuantity(productId, quantityChange);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpGet("check-availability/{productId}")]
        public async Task<ActionResult<bool>> CheckProductAvailability(int productId, [FromQuery] double requiredQuantity)
        {
            var result = await _inventoryService.CheckProductAvailability(productId, requiredQuantity);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}