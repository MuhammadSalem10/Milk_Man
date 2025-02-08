

namespace MilkMan.Shared.DTOs.Driver;

    public class DriverDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string IdentityImage { get; set; }
        public bool IsActive { get; set; } = true;
    }



