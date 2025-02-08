

using MilkMan.Domain.Common;

namespace MilkMan.Domain.Entities
{
    public class Review : AuditableEntity
    {
        public int CustomerId {  get; set; }
        public int ProductId { get; set; }
        public Customer Customer { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
    }
}
