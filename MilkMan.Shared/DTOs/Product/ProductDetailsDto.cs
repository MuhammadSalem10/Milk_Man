using System.ComponentModel.DataAnnotations;


namespace MilkMan.Shared.DTOs.Product
{
    public record ProductDetailsDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public MeasurementUnitDto Unit { get; set; }

        [Required]
        public double Quantity { get; set; }
        public int StockLevel { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [StringLength(2000)]
        public string Ingredients { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public decimal Discount { get; set; }
        public CategoryDto Category { get; set; }

    }
}


/*
 * This DTO could be used for scenarios where you want to expose more detailed
 * information about a product than what's included in the basic ProductDto.
It could include additional properties like product description, images, reviews, or related products.
 */