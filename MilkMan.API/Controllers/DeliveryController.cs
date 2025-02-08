using Microsoft.AspNetCore.Mvc;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.Enums;
using MilkMan.Shared.Parameters;

namespace MilkMan.API.Controllers;

    public class DeliveryController : ApiController
    {

    private readonly IDeliveryService _deliveryService;
    private readonly IOrderService _orderService;

    public DeliveryController(IDeliveryService deliveryService, IOrderService orderService)
    {
        _deliveryService = deliveryService;
        _orderService = orderService;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignDriverToOrder(int orderId, int driverId)
    {
        var result = await _deliveryService.AssignDriverToOrder(orderId, driverId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
    }

    [HttpPost("{deliveryId}/complete")]
    public async Task<IActionResult> MarkDeliveryAsCompleted(int deliveryId)
    {
        var result = await _deliveryService.MarkDeliveryAsCompleted(deliveryId);
        if (result.IsSuccess)
        {
            await _orderService.UpdateOrderStatusBasedOnDelivery(result.Value.OrderId, DeliveryStatus.Delivered);
            return Ok(result.Value);
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetDeliveryByOrderId(int orderId)
    {
        var result = await _deliveryService.GetDeliveryByOrderId(orderId);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
    }

    [HttpGet]
    public async Task<IActionResult> GetDeliveries([FromQuery] DeliveryQueryParameters queryParams)
    {
        var result = await _deliveryService.GetDeliveries(queryParams);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
    }

    [HttpPut("{deliveryId}/status")]
    public async Task<IActionResult> UpdateDeliveryStatus(int deliveryId, [FromBody] DeliveryStatus newStatus)
    {
        var result = await _deliveryService.UpdateDeliveryStatus(deliveryId, newStatus);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
    }

}

