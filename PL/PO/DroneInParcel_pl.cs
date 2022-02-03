using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class DroneInParcel_pl
    {
        public DroneInParcel_pl(DroneInParcel drone)
        {
            Id = drone.Id;
            BatteryStatus = drone.BatteryStatus;
            CurrentPosition = new Position_pl(drone.CurrentPosition);
        }
        public override string ToString()
        {
            return $"----------------\nID: {Id}\nBattery Status: {BatteryStatus}\nPosition - {CurrentPosition.ToString()}";
        }
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public Position_pl CurrentPosition { get; set; }
    }
}

