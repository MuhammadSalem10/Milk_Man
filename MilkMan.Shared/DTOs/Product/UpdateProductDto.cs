using System.ComponentModel.DataAnnotations;


namespace MilkMan.Shared.DTOs.Product
{
    public record UpdateProductDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)] // Assuming minimum price is 0.01
        public decimal Price { get; set; }
        [Required]
        [Range(1, 8, ErrorMessage = "Id is not existed")]
        public int UnitId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)] // Assuming minimum quantity is 0.01 (depending on Id)
        public double Quantity { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(2000)] // Assuming a longer limit for ingredients list
        public string Ingredients { get;  set; }
        public bool IsAvailable { get;  set; }
        public decimal Discount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int StockLevel { get; set; }
    }
}
