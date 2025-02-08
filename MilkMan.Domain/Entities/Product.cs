using MilkMan.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MilkMan.Domain.Entities
{
    public class Product : AuditableEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, Range(0, double.MaxValue)] 
        public decimal Price { get; set; }
        [Required]
        [Range(1, 8, ErrorMessage = "Id is not existed")]
        public int UnitId { get;  set; }
        [Required]
        public MeasurementUnit Unit { get; set; } 

        [Required]
        public double Quantity { get; set; }        
        [MaxLength(500)]
        public string Description { get; set; }

        [Url] 
        public string ImageUrl { get; set; }

        [StringLength(2000)] 
        public string Ingredients { get; set; }
        [Required]
        public bool IsAvailable { get;  set; }
        public decimal Discount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
