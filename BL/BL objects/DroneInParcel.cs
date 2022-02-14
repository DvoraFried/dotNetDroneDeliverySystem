using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInParcel
    {
        #region CTOR
        public DroneInParcel(Drone drone)
        {
            Id = drone.Id;
            BatteryStatus = drone.BatteryStatus;
            CurrentPosition = drone.CurrentPosition;
        }
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            return $"----------------\nID: {Id}\nBattery Status: {BatteryStatus}\nPosition - {CurrentPosition.ToString()}";
        }
        #endregion

        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public Position CurrentPosition { get; set; }
    }
}
