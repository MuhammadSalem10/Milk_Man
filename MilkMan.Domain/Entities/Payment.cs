using MilkMan.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MilkMan.Shared.Enums;

namespace MilkMan.Domain.Entities;

public class Payment : AuditableEntity
{
    public int OrderID { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

}

