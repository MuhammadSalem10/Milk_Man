using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkMan.Shared.DTOs.Driver
{
    public class CreateDriverDto
    {


        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+20|0)?(?:1[0125789]|2[0-3489]|0[106])[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(20)]
        public string IdentityNumber { get; set; }
        [Url]
        public string IdentityImage { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
