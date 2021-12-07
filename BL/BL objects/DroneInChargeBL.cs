using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInChargeBL
    {
        public DroneInChargeBL(int id,double batteryS)
        {
            this.Id = id;
            this.BatteryStatus = batteryS;
        }
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
    }
}
