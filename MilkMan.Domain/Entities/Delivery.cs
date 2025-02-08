using MilkMan.Domain.Common;
using MilkMan.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkMan.Domain.Entities;

public class Delivery : AuditableEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int? DriverId { get; set; }
    public Driver Driver { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public DeliveryStatus Status { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public string? Notes { get; set; }
    public decimal DeliveryFee { get; set; }
    public DateTime DeliveryTime { get; set; }
    public CustomerFeedback Feedback { get; set; }
}