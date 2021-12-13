using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public DroneInParcel(DroneBL drone)
        {
            Id = drone.getIdBL();
            BatteryStatus = drone.BatteryStatus;
            CurrentPosition = drone.CurrentPosition;
        }
        public override string ToString()
        {
            return $"ID: {Id} |^| Battery Status: {BatteryStatus} |^| Position -{CurrentPosition.ToString()}";
        }
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public Position CurrentPosition { get; set; }
    }
}
