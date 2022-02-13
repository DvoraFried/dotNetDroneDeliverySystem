using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInCharge
    {
        public DroneInCharge(Drone drone)
        {
            Id = drone.getIdBL();
            BatteryStatus = drone.BatteryStatus;
            enterTime = DateTime.Now;
        }
        public DroneInCharge(int id, DateTime enteredTime)
        {
            Id = id;
            enterTime = enteredTime;
        }
        public override string ToString()
        {
            return $"-----------\nID: {Id}\nBattery Status: {BatteryStatus}\n-----------";
        }
        public DateTime enterTime;
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
    }
}
