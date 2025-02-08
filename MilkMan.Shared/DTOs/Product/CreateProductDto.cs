
using System.ComponentModel.DataAnnotations;


namespace MilkMan.Shared.DTOs.Product;
public record CreateProductDto
{


    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    [Required]
    [Range(1, 8, ErrorMessage = "Id does not exist!")]
    public int UnitId { get; set; }
    [Required]
    public double Quantity { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    [StringLength(2000)]
    public string Ingredients { get; set; }
    [Required]
    public bool IsAvailable { get; set; }
    public decimal Discount { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public int StockLevel { get; set; }
}
