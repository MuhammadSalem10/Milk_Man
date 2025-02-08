

namespace MilkMan.Shared.DTOs.Inventory
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int ReorderPoint { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? PurchasePrice { get; set; }
    }
}
