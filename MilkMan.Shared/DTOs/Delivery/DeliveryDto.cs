

using MilkMan.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkMan.Shared.DTOs.Delivery;

public record DeliveryDto(int Id, int OrderId, int? DriverId, DeliveryStatus Status, decimal DeliveryFee, 
    DateTime DeliveryTime, CustomerFeedbackDto Feedback);

