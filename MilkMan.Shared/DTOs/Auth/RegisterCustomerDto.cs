using System.ComponentModel.DataAnnotations;
using MilkMan.Shared.DTOs.Address;

namespace MilkMan.Shared.DTOs.Auth
{
    public record RegisterCustomerDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+20|0)?(?:1[0125789]|2[0-3489]|0[106])[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
