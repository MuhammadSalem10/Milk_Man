

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MilkMan.Domain.Entities;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "The UserName must be between 1 and 50 characters long.")]
        public override string UserName { get; set; } // Override to apply validation

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public override string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+20|0)?(?:1[0125789]|2[0-3489]|0[106])[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
        public override string PhoneNumber { get; set; }
    }

