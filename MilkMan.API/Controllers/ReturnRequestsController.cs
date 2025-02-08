using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MilkMan.Application.Interfaces;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Returns;
using MilkMan.Shared.Enums;

namespace MilkMan.API.Controllers;

public class ReturnRequestsController : ApiController
{
    private readonly IReturnService _returnService;
    private readonly IMapper _mapper;

    public ReturnRequestsController(IReturnService returnService, IMapper mapper)
    {
        _returnService = returnService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReturnRequest(ReturnRequestDto returnRequestDto)
    {
        var returnRequest = _mapper.Map<ReturnRequest>(returnRequestDto);
        var result = await _returnService.CreateReturnRequestAsync(returnRequest);

        if (result.IsSuccess)
        {
            var returnRequestResponse = _mapper.Map<ReturnRequestResponseDto>(result.Value);
            return CreatedAtAction(nameof(GetReturnRequest), new { id = returnRequestResponse.Id }, returnRequestResponse);
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReturnRequest(int id)
    {
        var result = await _returnService.GetReturnRequestAsync(id);

        if (result.IsSuccess)
        {
            var returnRequestResponse = _mapper.Map<ReturnRequestResponseDto>(result.Value);
            return Ok(returnRequestResponse);
        }

        return NotFound(result.ErrorMessage);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetCustomerReturnRequests(int customerId)
    {
        var returnRequests = await _returnService.GetCustomerReturnRequestsAsync(customerId);
        var returnRequestResponses = _mapper.Map<IEnumerable<ReturnRequestResponseDto>>(returnRequests);
        return Ok(returnRequestResponses);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateReturnRequestStatus(int id, [FromBody] ReturnStatus status)
    {
        var result = await _returnService.UpdateReturnRequestStatusAsync(id, status);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return NotFound(result.ErrorMessage);
    }
[HttpPost("{id}/assign-driver/{driverId}")]
    public async Task<IActionResult> AssignDriverToReturnRequest(int id, int driverId)
    {
        var result = await _returnService.AssignDriverToReturnRequestAsync(id, driverId);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPatch("{id}/pickup-status")]
    public async Task<IActionResult> UpdatePickupStatus(int id, [FromBody] PickupStatusUpdateDto updateDto)
    {
        var result = await _returnService.UpdateReturnRequestPickupStatusAsync(id, updateDto.Status, updateDto.PickupDate);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("driver/{driverId}")]
    public async Task<IActionResult> GetDriverAssignedReturnRequests(int driverId)
    {
        var returnRequests = await _returnService.GetDriverAssignedReturnRequestsAsync(driverId);
        var returnRequestResponses = _mapper.Map<IEnumerable<ReturnRequestResponseDto>>(returnRequests);
        return Ok(returnRequestResponses);
    }
}
