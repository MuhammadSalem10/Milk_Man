using MilkMan.Shared.Enums;

namespace MilkMan.Shared.DTOs.Returns;

public class ReturnRequestResponseDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RequestDate { get; set; }
    public string Reason { get; set; }
    public ReturnStatus Status { get; set; }
    public List<ReturnItemResponseDto> Items { get; set; }
    public int? AssignedDriverId { get; set; }
    public DateTime? PickupDate { get; set; }
}
