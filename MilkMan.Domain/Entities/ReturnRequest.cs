using MilkMan.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using MilkMan.Domain.Common;


namespace MilkMan.Domain.Entities;

public class ReturnRequest : AuditableEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime RequestDate { get; set; }
    [Required]
    public ReturnReason Reason { get; set; }
    public string Description { get; set; }

    [Required]
    public RequestedAction RequestedAction { get; set; }

    [Required]
    public ReturnStatus Status { get; set; } = ReturnStatus.Pending;

    public List<ReturnItem> Items { get; set; }
    public int? AssignedDriverId { get; set; }
    public Driver AssignedDriver { get; set; }
    public DateTime? PickupDate { get; set; }
}

