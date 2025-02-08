

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MilkMan.Shared.Enums;
using MilkMan.Domain.Common;

namespace MilkMan.Domain.Entities;

public class Refund : BaseEntity
{
    public int ReturnRequestId { get; set; }
    public ReturnRequest ReturnRequest { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

    [StringLength(255)]
    public RefundStatus Status { get; set; }
}