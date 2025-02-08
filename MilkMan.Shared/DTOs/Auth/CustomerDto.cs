


using MilkMan.Shared.DTOs.Address;

namespace MilkMan.Shared.DTOs.Auth;

public class CustomerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public UserDto User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; }
    public AddressDto? Address { get; set; }
}