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
            Id = drone.Id;
            BatteryStatus = drone.BatteryStatus;
            EnterTime = DateTime.Now;
        }
        public DroneInCharge(int id, DateTime enteredTime)
        {
            Id = id;
            EnterTime = enteredTime;
        }
        public override string ToString()
        {
            return $"-----------\nID: {Id}\nBattery Status: {BatteryStatus}\n-----------";
        }
        public DateTime EnterTime;
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
    }
}
