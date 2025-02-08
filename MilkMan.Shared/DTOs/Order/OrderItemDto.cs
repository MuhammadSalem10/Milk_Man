

using System.ComponentModel.DataAnnotations;

namespace MilkMan.Shared.DTOs.Order;

public record OrderItemDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
}

