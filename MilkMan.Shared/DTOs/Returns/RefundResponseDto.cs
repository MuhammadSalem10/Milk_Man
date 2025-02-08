using MilkMan.Shared.Enums;

namespace MilkMan.Shared.DTOs.Returns;

public class RefundResponseDto
{
    public int Id { get; set; }
    public int ReturnRequestId { get; set; }
    public decimal Amount { get; set; }
    public DateTime RefundDate { get; set; }
    public RefundStatus Status { get; set; }
}