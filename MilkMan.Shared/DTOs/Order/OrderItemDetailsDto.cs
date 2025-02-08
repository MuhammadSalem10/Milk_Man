using MilkMan.Shared.DTOs.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkMan.Shared.DTOs.Order;


public record OrderItemDetailsDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public ProductSummaryDto Product { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
}
