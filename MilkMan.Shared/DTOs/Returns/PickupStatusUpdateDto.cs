using MilkMan.Shared.Enums;


namespace MilkMan.Shared.DTOs.Returns
{
    public class PickupStatusUpdateDto
    {
        public ReturnStatus Status { get; set; }
        public DateTime PickupDate { get; set; }
    }
}
