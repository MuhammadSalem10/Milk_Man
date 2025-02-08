using MilkMan.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace MilkMan.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
