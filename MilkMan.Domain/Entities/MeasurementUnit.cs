using System.ComponentModel.DataAnnotations;

namespace MilkMan.Domain.Entities
{
    public class MeasurementUnit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(5)]
        public string? Symbol { get; set; } = string.Empty;
    }
}
