using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MilkMan.Domain.Common;

namespace MilkMan.Domain.Entities;

public class ReturnItem : BaseEntity
{
    public int ReturnRequestId { get; set; }
    public ReturnRequest ReturnRequest { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
