using MilkMan.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkMan.Shared.Parameters
{
    public class DeliveryQueryParameters
    {
        public DeliveryStatus? Status { get; set; }
        public int? DriverId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
