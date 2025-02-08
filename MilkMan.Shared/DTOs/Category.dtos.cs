

using System.ComponentModel.DataAnnotations;

namespace MilkMan.Shared.DTOs;

public record CategoryDto(int Id, string Name);

public record CreateCategoryDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}

public record UpdateCategoryDto(int Id, string Name);


