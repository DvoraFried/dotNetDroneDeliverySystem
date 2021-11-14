using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DeliveyByTransfer
    {
        int Id { get; set; }
        EnumBL.WeightCategoriesBL Weight { get; set; }
        EnumBL.PrioritiesBL Priority { get; set; }
        bool DeliveryStatus { get; set; }
        Position CollectionLocation { get; set; }
        Position DeliveryDestinationLocation { get; set; }

        //Distance TransportDistance { get; set; }//creat a function that calculate a distance
    }
}
