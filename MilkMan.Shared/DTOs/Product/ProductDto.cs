using System.ComponentModel.DataAnnotations;


namespace MilkMan.Shared.DTOs.Product;

    public record ProductDto (int Id, string Name, decimal Price, MeasurementUnitDto Unit, double Quantity, string ImageUrl, decimal Discount, int StockLevel);
   
