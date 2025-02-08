

namespace MilkMan.Shared.DTOs.Inventory;

    public class CreateInventoryDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public int ReorderPoint { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? PurchasePrice { get; set; }
    }

