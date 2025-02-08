

using System.Text.Json.Serialization;

namespace MilkMan.Shared.DTOs;

public record RegisterDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

}

public record LoginDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

}

public record ResetPasswordDto(string UserEmail, string Token, string NewPassword);
