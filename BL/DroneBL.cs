using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    public class DroneBL
    {
        public int IdBL { get; set; }
        public string ModelBL { get; set; }
        public WeightCategoriesBL MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        //Drone status
        //DeliveryByTransfer
        Position CurrentPosition { get; set; }

    }
}
