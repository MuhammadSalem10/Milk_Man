

using System.ComponentModel.DataAnnotations;

namespace MilkMan.Shared.DTOs.Auth;

public class UpdateCustomerDto
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    [StringLength(50)]
    public string? LastName { get; set; }
    [Required]
    [RegularExpression(@"^(?:\+20|0)?(?:1[0125789]|2[0-3489]|0[106])[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}

