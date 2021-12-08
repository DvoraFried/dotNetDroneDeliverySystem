using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneToList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        EnumBL.WeightCategoriesBL Weight { get; set; }
        public double BatteryStatus { get; set; }
        //Drone status
        Position CurrentPosition { get; set; }
        public int ParcelNun { get; set; }
    }
}