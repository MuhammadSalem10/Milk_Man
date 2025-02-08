

using MilkMan.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkMan.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductID { get; set; } 
        public string ProductName { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }

}
