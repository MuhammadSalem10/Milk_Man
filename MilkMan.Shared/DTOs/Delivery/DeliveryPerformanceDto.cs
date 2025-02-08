namespace MilkMan.Shared.DTOs.Delivery;

public record DeliveryPerformanceDto(DateTime Date, int CompletedDeliveries, double AverageRating, decimal TotalEarnings);
