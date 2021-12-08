using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneInParcel
    {
        int Id { get; set; }
        double BatteryStatus { get; set; }
        Position CurrentPosition { get; set; }
    }
}
