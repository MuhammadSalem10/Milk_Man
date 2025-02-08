using MilkMan.Domain.Common;
using MilkMan.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace MilkMan.Domain.Entities;

public class Order : AuditableEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public OrderStatus Status { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItem> Items { get; set; } = [];
    public Delivery Delivery { get; set; }
}


