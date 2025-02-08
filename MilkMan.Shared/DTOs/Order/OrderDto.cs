

using MilkMan.Shared.DTOs.Auth;
using MilkMan.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkMan.Shared.DTOs.Order;

public record OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CustomerDto Customer { get; set; }
    public OrderStatus Status { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItemDto> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}