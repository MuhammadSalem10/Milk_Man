

namespace MilkMan.Shared.DTOs.Inventory;

    public class UpdateInventoryDto
    {
        public decimal Quantity { get; set; }
        public int ReorderPoint { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? PurchasePrice { get; set; }
    }

