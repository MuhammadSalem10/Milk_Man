using MilkMan.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace MilkMan.Domain.Entities;

    public class ShoppingCart : BaseEntity
    {

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public  List<CartItem> CartItems { get; set; } 

    }

