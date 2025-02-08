using MilkMan.Shared.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkMan.Shared.DTOs.Auth
{
    public class CustomerSummaryDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public bool IsActive { get; set; } 
    }

}
