using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInChargeBL
    {
        public DroneInChargeBL(DroneBL drone)
        {
            Id = drone.getIdBL();
            BatteryStatus = drone.BatteryStatus;
        }
        public override string ToString()
        {
            return $"-----------\nID: {Id}\nBattery Status: {BatteryStatus}\n-----------";
        }
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
    }
}
