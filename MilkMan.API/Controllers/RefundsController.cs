using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MilkMan.Shared.DTOs.Returns;

namespace MilkMan.API.Controllers;
public class RefundsController : ApiController
{
    private readonly IRefundService _refundService;
    private readonly IMapper _mapper;

    public RefundsController(IRefundService refundService, IMapper mapper)
    {
        _refundService = refundService;
        _mapper = mapper;
    }

    [HttpPost("process/{returnRequestId}")]
    public async Task<IActionResult> ProcessRefund(int returnRequestId)
    {
        var result = await _refundService.ProcessRefundAsync(returnRequestId);

        if (result.IsSuccess)
        {
            var refundResponse = _mapper.Map<RefundResponseDto>(result.Value);
            return CreatedAtAction(nameof(GetRefund), new { returnRequestId = refundResponse.ReturnRequestId }, refundResponse);
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("{returnRequestId}")]
    public async Task<IActionResult> GetRefund(int returnRequestId)
    {
        var result = await _refundService.GetRefundByReturnRequestIdAsync(returnRequestId);

        if (result.IsSuccess)
        {
            var refundResponse = _mapper.Map<RefundResponseDto>(result.Value);
            return Ok(refundResponse);
        }

        return NotFound(result.ErrorMessage);
    }
}