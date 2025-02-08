

using System.ComponentModel.DataAnnotations;

namespace MilkMan.Shared.DTOs.Auth;

public record UserDto(string Id, string UserName, string Email, string PhoneNumber);