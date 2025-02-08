
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MilkMan.Domain.Common;


namespace MilkMan.Domain.Entities
{
    public class Inventory : AuditableEntity
    {
        public int ProductId { get; set; }

        [Required]
        public double Quantity { get; set; }  

        [Required]
        public int ReorderPoint { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PurchasePrice { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

}
