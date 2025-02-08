using System.ComponentModel.DataAnnotations;
using MilkMan.Domain.Common;

namespace MilkMan.Domain.Entities;

public class Driver : AuditableEntity
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    public ApplicationUser User { get; set; }

    [Required]
    [StringLength(20)]
    public string IdentityNumber { get; set; }

    [Url]
    public string IdentityImage { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<Order> AssignedOrders { get; set; }
}