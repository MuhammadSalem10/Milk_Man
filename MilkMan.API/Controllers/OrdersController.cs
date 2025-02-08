using Microsoft.AspNetCore.Mvc;
using MilkMan.Shared.DTOs.Order;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.Enums;
using MilkMan.Application.Parameters;

namespace MilkMan.API.Controllers;

public class OrdersController : ApiController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("place-order")]
    public async Task<ActionResult<OrderDto>> CreateOrder(PlaceOrderDto placeOrderDto)
    {
        var result = await _orderService.PlaceOrder(placeOrderDto);
        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }
        return CreatedAtAction(nameof(GetOrderById), new { id = result.Value.Id }, result.Value);
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
    {
        var order = await _orderService.GetOrderById(orderId, trackChanges: false);

        if (order == null)
            return NotFound("Order is not found!");

        return Ok(order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersForCustomer(int customerId)
    {
        var order = await _orderService.GetOrdersByCustomersId(customerId, trackChanges: false);
        if (order == null)
            return NotFound("Order is not found!");

        return Ok(order);
    }

    [HttpGet("/order-statuses")]
    public IActionResult GetOrderStatuses()
    {
        return Ok(Enum.GetValues<OrderStatus>()); // Get all enum values
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus newStatus)
    {
        var result = await _orderService.UpdateOrderStatus(id, newStatus);
        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }
        return NoContent();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        var result = await _orderService.CancelOrder(id);
        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }
        return NoContent();
    }

    [HttpGet("yesterday")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetYesterdaysOrders()
    {
       var orders = await _orderService.GetYesterdaysOrders();
        
       return Ok(orders);
    }
    [HttpGet]
    public async Task<ActionResult<PagedResult<OrderDto>>> GetOrders([FromQuery] OrderQueryParameters queryParams)
    {
        var result = await _orderService.GetOrders(queryParams);
        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyOrder(int id, ModifyOrderDto modifyOrderDto)
    {
        var result = await _orderService.ModifyOrder(id, modifyOrderDto);
        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }
        return NoContent();
    }


}

