
namespace MilkMan.Shared.DTOs.Product
{
    public record ProductSummaryDto(int Id, string Name, MeasurementUnitDto Unit, double Quantity, string ImageUrl, decimal Discount);

}
