using MilkMan.Domain.Common;

namespace MilkMan.Domain.Entities;

public class CustomerFeedback : BaseEntity
{
    public int DeliveryId { get; set; }
    public Delivery Delivery { get; set; }
    public string Comment { get; set; }
    public int Rating
    {
        get; set;
    }
}