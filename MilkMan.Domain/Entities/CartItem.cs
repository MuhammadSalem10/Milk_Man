
using MilkMan.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MilkMan.Domain.Entities;

public class CartItem : BaseEntity
{
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; }
    [Required, Range(0, 10000)]
    public int Quantity { get; set; }

}

