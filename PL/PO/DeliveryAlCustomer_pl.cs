using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class DeliveryAlCustomer_pl
    {
        public int Id { get; set; }
        Enum_pl.WeightCategories Weight { get; set; }
        Enum_pl.Priorities Priority { get; set; }
        Enum_pl.DeliveryStatus Status { get; set; }
        CustomerOnDelivery_pl Customer { get; set; }

    }
}
