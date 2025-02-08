namespace MilkMan.Shared.DTOs.Returns;

public class ReturnRequestDto
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string Reason { get; set; }
    public List<ReturnItemDto> Items { get; set; }
}
