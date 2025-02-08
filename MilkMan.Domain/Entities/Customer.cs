

namespace MilkMan.Domain.Entities;

using MilkMan.Domain.Common;
using System.ComponentModel.DataAnnotations;

public class Customer : AuditableEntity
{
    
    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; } = true;
    public Address Address { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public List<Order> Orders { get; set; } 
   
}

