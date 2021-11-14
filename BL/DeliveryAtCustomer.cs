using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DeliveryAtCustomer
    { 
        int Id { get; set; }
        EnumBL.WeightCategoriesBL Weight { get; set; }
        EnumBL.PrioritiesBL Priority { get; set; }
        EnumBL.DroneStatusesBL Status { get; set; }

    }
}
